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
using static FlightAgreementApplication.DTO.Response.AirlineManagerOperationsResponse;

namespace FlightAgreementApplication.FAAData
{
    public class FAAAirlineManagerOperationsData
    {
        private readonly FlightAgreementAppContext _context;
        public FAAAirlineManagerOperationsData(FlightAgreementAppContext context)
        {
            _context = context;
        }

        //public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators()
        //{
        //    try
        //    {

        //        var tourOperators = _context.TourOperators.ToList();
        //        return tourOperators;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public ActionResult<GetAllTourOperatorsResponse> GetAllTourOperators(
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


                var response = new GetAllTourOperatorsResponse
                {
                    TotalRows = totalRowCount,
                    TourOperators = tourOperators,
                };

                return response;
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
                    ActivityStatus = IsActive.Active,
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
                    ActivityStatus = IsActive.Active,
                    //ActivationToken = GenerateToken(),
                    //ActivationTokenExpiry = DateTime.UtcNow.AddHours(24),
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

                //var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse("ccchinmaysinnn@gmail.com"));
                //email.To.Add(MailboxAddress.Parse(user.UserEmail));
                //email.Subject = "FAA Account Activation";

                //var bodyBuilder = new BodyBuilder
                //{
                //    TextBody = $"Your activation token is: {user.ActivationToken}"
                //};
                //email.Body = bodyBuilder.ToMessageBody();

                //using var smtp = new SmtpClient();
                //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //smtp.Authenticate("ccchinmaysinnn@gmail.com", "vcxe hoqo qlvp hekw");
                //smtp.Send(email);
                //smtp.Disconnect(true);

                string messege = "Tour Operator added successfully.";
                
                return messege;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string UpdateTourOperator(Guid tourOperatorId, updateTourOperator updatedTourOperatorDto)
        {
            try
            {

                var existingTourOperator = _context.TourOperators.FirstOrDefault(to => to.TourOperatorId == tourOperatorId);

                if (existingTourOperator == null)
                {
                    return ($"Tour Operator with ID {tourOperatorId} not found.");
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

                return ("Tour Operator updated successfully.");
            }
            catch (Exception ex)
            {
                return ( "An error occurred while processing your request.");
            }

        }


        public String DeleteTourOperator(Guid tourOperatorId)
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
                    return ($"Tour Operator with ID {tourOperatorId} not found.");
                }

                _context.TourOperators.Remove(tourOperatorToDelete);
                _context.SaveChanges();

                return ("Tour Operator deleted successfully.");
            }
            catch (Exception ex)
            {
                return ( "An error occurred while processing your request.");
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
