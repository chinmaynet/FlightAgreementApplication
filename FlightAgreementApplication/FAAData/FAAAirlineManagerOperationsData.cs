using FlightAgreementApplication.DTO.Request;
using FlightAgreementApplication.DTO;
using FlightAgreementApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FlightAgreementApplication.Data;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using FlightAgreementApplication.DTO.Response;
using MailKit.Net.Smtp;

namespace FlightAgreementApplication.FAAData
{
    public class FAAAirlineManagerOperationsData
    {
        private readonly FlightAgreementAppContext _context;
        public FAAAirlineManagerOperationsData(FlightAgreementAppContext context)
        {
            _context = context;
        }

        public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators()
        {
            try
            {

                var tourOperators = _context.TourOperators.ToList();
                return tourOperators;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult<TourOperator> GetTourOperatorById(Guid tourOperatorId)
        {
            try
            {
                var tourOperator = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

                if (tourOperator == null)
                {
                    return new NotFoundObjectResult(new { Message = $"Tour Operator with ID {tourOperatorId} not found." });
                }

                return tourOperator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> AddTourOperator(TourOperatorDto newTourOperatorDto)
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
                await _context.SaveChangesAsync();


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
                await _context.SaveChangesAsync();

                var tourOperatorRole = _context.Roles.FirstOrDefault(r => r.RoleName == "TourOperator");
                if (tourOperatorRole == null)
                {
                    return ("Role 'TourOperator' not found.");
                }

                var userRole = new UserRole
                {
                    UserRoleId = Guid.NewGuid(),
                    UId = user.Id,
                    RId = tourOperatorRole.Id,
                    TourOperatorId = newTourOperator.TourOperatorId,
                };

                _context.UserRole.Add(userRole);
                await _context.SaveChangesAsync();



                var responseData = new TourOperatorCreateResponse
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

                string messege = "Tour Operator added successfully.";
                
                return messege;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public IActionResult UpdateTourOperator(Guid tourOperatorId, updateTourOperator updatedTourOperatorDto)
        //{
        //}


        //public IActionResult DeleteTourOperator(Guid tourOperatorId)
        //{
        //}




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
