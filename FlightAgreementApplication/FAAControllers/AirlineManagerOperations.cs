using FlightAgreementApplication.Data;
using FlightAgreementApplication.DTO;
using FlightAgreementApplication.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FlightAgreementApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
 
    public class AirlineManagerOperations : ControllerBase
    {

        private readonly FlightAgreementAppContext _context;
        public AirlineManagerOperations(FlightAgreementAppContext context)
        {
            _context = context;
        }
        
        [HttpGet("GetTourOperators")]
        
        public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators()
        {
            try
            {
                
                var tourOperators = _context.TourOperators.ToList();
                return Ok(tourOperators);
            }
            catch
            {
                 return Unauthorized( "An error occured while processing your request");
                //throw;
            }
        }

        [HttpGet("GetTourOperatorById/{tourOperatorId}")]
        public ActionResult<TourOperator> GetTourOperatorById(Guid tourOperatorId)
        {
            try
            {
                var tourOperator = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

                if (tourOperator == null)
                {
                    return NotFound($"Tour Operator with ID {tourOperatorId} not found.");
                }

                return Ok(tourOperator);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("AddTourOperator")]
        public IActionResult AddTourOperator([FromBody] TourOperatorDto newTourOperatorDto)
        {
            try
            {
                
                var newTourOperator = new TourOperator
                {
                    TourOperatorId = Guid.NewGuid(),
                    TourOperatorName = newTourOperatorDto.TourOperatorName,
                    TourOperatorAddress = newTourOperatorDto.TourOperatorAddress,
                    TourOperatorEmail = newTourOperatorDto.TourOperatorEmail,
                    TourOperatorPhone = newTourOperatorDto.TourOperatorPhone,
                    TourOperatorLandLine = newTourOperatorDto.TourOperatorLandLine,
                    TourOperatorContactPreferences = (ContactPreference)newTourOperatorDto.TourOperatorContactPreferences,              
                    AddedBy = "AirlineManager",
                };
               
                _context.TourOperators.Add(newTourOperator);
                _context.SaveChanges();


                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = newTourOperatorDto.TourOperatorName,
                    UserEmail = newTourOperatorDto.TourOperatorEmail,
                    //UserPassword = tourOperatorDto.TourOperatorPassword,
                    ActivityStatus = IsActive.Inactive,
                    ActivationToken = GenerateToken(),
                    ActivationTokenExpiry = DateTime.UtcNow.AddHours(24),
                };

                var passwordHasher = new PasswordHasher<User>();
                user.UserPassword = passwordHasher.HashPassword(user, newTourOperatorDto.TourOperatorPassword);

                _context.users.Add(user);
                _context.SaveChangesAsync();

                var tourOperatorRole = _context.Roles.FirstOrDefault(r => r.RoleName == "TourOperator");
                if (tourOperatorRole == null)
                {
                    return NotFound("Role 'TourOperator' not found."); 
                }
                
                var userRole = new UserRole
                {
                    UserRoleId = Guid.NewGuid(),
                    UId = user.Id,
                    RId = tourOperatorRole.Id, 
                    TourOperatorId = newTourOperator.TourOperatorId,
                };

                _context.UserRole.Add(userRole);
                _context.SaveChangesAsync();



                var responseData = new
                {
                    TourOperatorId = newTourOperator.TourOperatorId,
                    TourOperatorName = newTourOperator.TourOperatorName,
                    TourOperatorAddress = newTourOperator.TourOperatorAddress,
                    TourOperatorEmail = newTourOperator.TourOperatorEmail,
                    TourOperatorPhone = newTourOperator.TourOperatorPhone,
                    TourOperatorLandLine = newTourOperator.TourOperatorLandLine,
                    TourOperatorContactPreferences = newTourOperator.TourOperatorContactPreferences,
                    AddedBy = newTourOperator.AddedBy,
                    UserRoleId = userRole.UserRoleId,
                    UId = userRole.UId,
                    RId = userRole.RId,
                    UserRole = user.UserRoles?.FirstOrDefault()?.Role?.RoleName
                };

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("ccchinmaysinnn@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.UserEmail));
                email.Subject = "FAA Account Activation";

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = $"Your activation token is: {user.ActivationToken}"
                };
                email.Body = bodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("ccchinmaysinnn@gmail.com", "vcxe hoqo qlvp hekw");
                smtp.Send(email);
                smtp.Disconnect(true);

                return Ok("Tour Operator added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("UpdateTourOperator/{tourOperatorId}")]
        public IActionResult UpdateTourOperator(Guid tourOperatorId, [FromBody] updateTourOperator updatedTourOperatorDto)
        {
            try
            {
                
                var existingTourOperator = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

                if (existingTourOperator == null)
                {
                    return NotFound($"Tour Operator with ID {tourOperatorId} not found.");
                }
             
                existingTourOperator.TourOperatorName = updatedTourOperatorDto.TourOperatorName;
                existingTourOperator.TourOperatorAddress = updatedTourOperatorDto.TourOperatorAddress;
                existingTourOperator.TourOperatorEmail = updatedTourOperatorDto.TourOperatorEmail;
                existingTourOperator.TourOperatorPhone = updatedTourOperatorDto.TourOperatorPhone;
                existingTourOperator.TourOperatorLandLine = updatedTourOperatorDto.TourOperatorLandLine;
                existingTourOperator.TourOperatorContactPreferences = updatedTourOperatorDto.TourOperatorContactPreferences;

                existingTourOperator.ModifiedBy = "AirlineManager"; 
                existingTourOperator.ModifyDate = DateTime.UtcNow;

                _context.SaveChanges();

                return Ok("Tour Operator updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("DeleteTourOperator/{tourOperatorId}")]
        public IActionResult DeleteTourOperator(Guid tourOperatorId)
        {
            try
            {
               
                var userRole = _context.UserRole.FirstOrDefault(userRole => userRole.TourOperatorId == tourOperatorId);
                    
                _context.UserRole.Remove(userRole);
                _context.SaveChanges();

                var user = _context.users.FirstOrDefault(user => user.Id == userRole.UId);

                _context.users.Remove(user);
                _context.SaveChanges();

                var tourOperatorToDelete = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

                if (tourOperatorToDelete == null)
                {
                    return NotFound($"Tour Operator with ID {tourOperatorId} not found.");
                }
               
                _context.TourOperators.Remove(tourOperatorToDelete);
                _context.SaveChanges();

                return Ok("Tour Operator deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
