using FlightAgreementApplication.Data;
using FlightAgreementApplication.DTO;
using FlightAgreementApplication.DTO.Request;
using FlightAgreementApplication.FAAWrapper;
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
using static FlightAgreementApplication.DTO.Response.AirlineManagerOperationsResponse;

namespace FlightAgreementApplication.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AirlineManagerOperations : ControllerBase
    {

        private readonly FlightAgreementAppContext _context;

        private readonly FAAAirlineManagerOperationsWrapper _wrapper;
        public AirlineManagerOperations(FlightAgreementAppContext context, FAAAirlineManagerOperationsWrapper wrapper)
        {
            _context = context;
            _wrapper = wrapper;
        }

        //[HttpGet("GetTourOperators")]
        //public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators()
        //{
        //    try
        //    {
        //        var tourOperators = _wrapper.GetAllTourOperators();
        //        return Ok(tourOperators.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
        //    }
        //}


        [HttpGet("GetTourOperators")]
        public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators(
            string sortColumn = "tourOperatorName",
            string sortOrder = "asc",
            string searchTerm = "",
            int page = 1,
            int pageSize = 5)
        {
            try
            {

                var tourOperators = _context.TourOperators.ToList();


                tourOperators = SearchTourOperators(tourOperators, searchTerm);


                tourOperators = SortTourOperators(tourOperators, sortColumn, sortOrder);


                var totalRowCount = tourOperators.Count;


                var startIndex = (page - 1) * pageSize;
                tourOperators = tourOperators.Skip(startIndex).Take(pageSize).ToList();


                var response = new
                {
                    TotalRows = totalRowCount,
                    TourOperators = tourOperators
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }

        private List<TourOperator> SearchTourOperators(List<TourOperator> tourOperators, string searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                tourOperators = tourOperators.Where(o =>
            o.TourOperatorName.ToLower().Contains(searchTerm) ||
                    o.TourOperatorEmail.ToLower().Contains(searchTerm) ||
                    o.TourOperatorAddress.ToLower().Contains(searchTerm) ||
                    o.TourOperatorPhone.ToLower().Contains(searchTerm) ||
                    o.TourOperatorLandLine.ToLower().Contains(searchTerm) ||
                      o.TourOperatorContactPreferences.ToString().ToLower().Contains(searchTerm)

                ).ToList();
            }

            return tourOperators;
        }

        private List<TourOperator> SortTourOperators(List<TourOperator> tourOperators, string sortColumn, string sortOrder)
        {

            switch (sortColumn)
            {
                case "tourOperatorName":
                    tourOperators = sortOrder == "asc"
                        ? tourOperators.OrderBy(o => o.TourOperatorName).ToList()
                        : tourOperators.OrderByDescending(o => o.TourOperatorName).ToList();
                    break;
                case "tourOperatorEmail":
                    tourOperators = sortOrder == "asc"
                         ? tourOperators.OrderBy(o => o.TourOperatorEmail).ToList()
                         : tourOperators.OrderByDescending(o => o.TourOperatorEmail).ToList();
                    break;
                case "tourOperatorAddress":
                    tourOperators = sortOrder == "asc"
                         ? tourOperators.OrderBy(o => o.TourOperatorAddress).ToList()
                         : tourOperators.OrderByDescending(o => o.TourOperatorAddress).ToList();
                    break;
                case "tourOperatorPhone":
                    tourOperators = sortOrder == "asc"
                         ? tourOperators.OrderBy(o => o.TourOperatorPhone).ToList()
                         : tourOperators.OrderByDescending(o => o.TourOperatorPhone).ToList();
                    break;
                case "tourOperatorLandLine":
                    tourOperators = sortOrder == "asc"
                         ? tourOperators.OrderBy(o => o.TourOperatorLandLine).ToList()
                         : tourOperators.OrderByDescending(o => o.TourOperatorLandLine).ToList();
                    break;
                case "tourOperatorContactPreferences":
                    tourOperators = sortOrder == "asc"
                         ? tourOperators.OrderBy(o => o.TourOperatorContactPreferences).ToList()
                         : tourOperators.OrderByDescending(o => o.TourOperatorContactPreferences).ToList();
                    break;

            }

            return tourOperators;
        }

        //public ActionResult<IEnumerable<GetAllTourOperatorsResponse>> GetAllTourOperators(
        //    string sortColumn = "tourOperatorName",
        //    string sortOrder = "asc",
        //    string searchTerm = "",
        //    int page = 1,
        //    int pageSize = 5)
        //{
        //    try
        //    {
        //        var tourOperators = _wrapper.GetAllTourOperators();
        //        var response = new GetAllTourOperatorsResponse
        //        {
        //            TotalRows = tourOperators.Value.TotalRows,
        //            TourOperators = tourOperators.Value.TourOperators,
        //        };
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
        //    }
        //}

        //public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators()
        //{
        //    try
        //    {

        //        var tourOperators = _context.TourOperators.ToList();
        //        return Ok(tourOperators);
        //    }
        //    catch
        //    {
        //        return Unauthorized("An error occured while processing your request");
        //        throw;
        //    }
        //}

        [HttpGet("GetTourOperatorById/{tourOperatorId}")]

        public ActionResult<TourOperator> GetTourOperatorById(Guid tourOperatorId)
        {
            try
            {
                var tourOperator = _wrapper.GetTourOperatorById(tourOperatorId);
                return Ok(tourOperator.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //public ActionResult<TourOperator> GetTourOperatorById(Guid tourOperatorId)
        //{
        //    try
        //    {
        //        var tourOperator = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

        //        if (tourOperator == null)
        //        {
        //            return NotFound($"Tour Operator with ID {tourOperatorId} not found.");
        //        }

        //        return Ok(tourOperator);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

        [HttpPost("AddTourOperator")]
        public async Task<IActionResult> AddTourOperator([FromBody] TourOperatorDto newTourOperatorDto)
        {
            try
            {
                var addTourOperatorResponse = await _wrapper.AddTourOperator(newTourOperatorDto);

                return Ok(addTourOperatorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }



        //[HttpPost("AddTourOperator")]
        //public IActionResult AddTourOperator([FromBody] TourOperatorDto newTourOperatorDto)
        //{
        //    try
        //    {

        //        var newTourOperator = new TourOperator
        //        {
        //            TourOperatorId = Guid.NewGuid(),
        //            TourOperatorName = newTourOperatorDto.TourOperatorName,
        //            TourOperatorAddress = newTourOperatorDto.TourOperatorAddress,
        //            TourOperatorEmail = newTourOperatorDto.TourOperatorEmail,
        //            TourOperatorPhone = newTourOperatorDto.TourOperatorPhone,
        //            TourOperatorLandLine = newTourOperatorDto.TourOperatorLandLine,
        //            TourOperatorContactPreferences = (ContactPreference)newTourOperatorDto.TourOperatorContactPreferences,              
        //            AddedBy = "AirlineManager",
        //        };

        //        _context.TourOperators.Add(newTourOperator);
        //        _context.SaveChanges();


        //        var user = new User
        //        {
        //            Id = Guid.NewGuid(),
        //            UserName = newTourOperatorDto.TourOperatorName,
        //            UserEmail = newTourOperatorDto.TourOperatorEmail,
        //            //UserPassword = tourOperatorDto.TourOperatorPassword,
        //            ActivityStatus = IsActive.Inactive,
        //            ActivationToken = GenerateToken(),
        //            ActivationTokenExpiry = DateTime.UtcNow.AddHours(24),
        //        };

        //        var passwordHasher = new PasswordHasher<User>();
        //        user.UserPassword = passwordHasher.HashPassword(user, newTourOperatorDto.TourOperatorPassword);

        //        _context.users.Add(user);
        //        _context.SaveChangesAsync();

        //        var tourOperatorRole = _context.Roles.FirstOrDefault(r => r.RoleName == "TourOperator");
        //        if (tourOperatorRole == null)
        //        {
        //            return NotFound("Role 'TourOperator' not found."); 
        //        }

        //        var userRole = new UserRole
        //        {
        //            UserRoleId = Guid.NewGuid(),
        //            UId = user.Id,
        //            RId = tourOperatorRole.Id, 
        //            TourOperatorId = newTourOperator.TourOperatorId,
        //        };

        //        _context.UserRole.Add(userRole);
        //        _context.SaveChangesAsync();



        //        var responseData = new
        //        {
        //            TourOperatorId = newTourOperator.TourOperatorId,
        //            TourOperatorName = newTourOperator.TourOperatorName,
        //            TourOperatorAddress = newTourOperator.TourOperatorAddress,
        //            TourOperatorEmail = newTourOperator.TourOperatorEmail,
        //            TourOperatorPhone = newTourOperator.TourOperatorPhone,
        //            TourOperatorLandLine = newTourOperator.TourOperatorLandLine,
        //            TourOperatorContactPreferences = newTourOperator.TourOperatorContactPreferences,
        //            AddedBy = newTourOperator.AddedBy,
        //            UserRoleId = userRole.UserRoleId,
        //            UId = userRole.UId,
        //            RId = userRole.RId,
        //            UserRole = user.UserRoles?.FirstOrDefault()?.Role?.RoleName
        //        };

        //        var email = new MimeMessage();
        //        email.From.Add(MailboxAddress.Parse("ccchinmaysinnn@gmail.com"));
        //        email.To.Add(MailboxAddress.Parse(user.UserEmail));
        //        email.Subject = "FAA Account Activation";

        //        var bodyBuilder = new BodyBuilder
        //        {
        //            TextBody = $"Your activation token is: {user.ActivationToken}"
        //        };
        //        email.Body = bodyBuilder.ToMessageBody();

        //        using var smtp = new SmtpClient();
        //        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        //        smtp.Authenticate("ccchinmaysinnn@gmail.com", "vcxe hoqo qlvp hekw");
        //        smtp.Send(email);
        //        smtp.Disconnect(true);

        //        return Ok("Tour Operator added successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

        //[HttpPut("UpdateTourOperator/{tourOperatorId}")]
        //public IActionResult UpdateTourOperator(Guid tourOperatorId, [FromBody] updateTourOperator updatedTourOperatorDto)
        //{
        //    try
        //    {

        //        var existingTourOperator = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

        //        if (existingTourOperator == null)
        //        {
        //            return NotFound($"Tour Operator with ID {tourOperatorId} not found.");
        //        }

        //        existingTourOperator.TourOperatorName = updatedTourOperatorDto.TourOperatorName;
        //        existingTourOperator.TourOperatorAddress = updatedTourOperatorDto.TourOperatorAddress;
        //        existingTourOperator.TourOperatorEmail = updatedTourOperatorDto.TourOperatorEmail;
        //        existingTourOperator.TourOperatorPhone = updatedTourOperatorDto.TourOperatorPhone;
        //        existingTourOperator.TourOperatorLandLine = updatedTourOperatorDto.TourOperatorLandLine;
        //        existingTourOperator.TourOperatorContactPreferences = updatedTourOperatorDto.TourOperatorContactPreferences;

        //        existingTourOperator.ModifiedBy = "AirlineManager";
        //        existingTourOperator.ModifyDate = DateTime.UtcNow;

        //        _context.SaveChanges();

        //        return Ok("Tour Operator updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

        [HttpPut("UpdateTourOperator/{tourOperatorId}")]
        public string UpdateTourOperator(Guid tourOperatorId, [FromBody] updateTourOperator updatedTourOperatorDto)
        {
            var msg = _wrapper.UpdateTourOperator(tourOperatorId, updatedTourOperatorDto);

            return msg;
        }

        //[HttpDelete("DeleteTourOperator/{tourOperatorId}")]
        //public IActionResult DeleteTourOperator(Guid tourOperatorId)
        //{
        //    try
        //    {

        //        var userRole = _context.UserRole.FirstOrDefault(userRole => userRole.TourOperatorId == tourOperatorId);

        //        _context.UserRole.Remove(userRole);
        //        _context.SaveChanges();

        //        var user = _context.users.FirstOrDefault(user => user.Id == userRole.UId);

        //        _context.users.Remove(user);
        //        _context.SaveChanges();

        //        var tourOperatorToDelete = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

        //        if (tourOperatorToDelete == null)
        //        {
        //            return NotFound($"Tour Operator with ID {tourOperatorId} not found.");
        //        }

        //        _context.TourOperators.Remove(tourOperatorToDelete);
        //        _context.SaveChanges();

        //        return Ok("Tour Operator deleted successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}



        [HttpDelete("DeleteTourOperator/{tourOperatorId}")]
        public String DeleteTourOperator(Guid tourOperatorId)
        {
            var msg = _wrapper.DeleteTourOperator(tourOperatorId);
            return msg;
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
