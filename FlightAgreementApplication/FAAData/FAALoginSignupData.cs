using FlightAgreementApplication.Data;
using FlightAgreementApplication.DTO.Request;
using FlightAgreementApplication.DTO.Response;
using FlightAgreementApplication.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightAgreementApplication.Data;
using FlightAgreementApplication.Model;
using FlightAgreementApplication.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Text;
using MimeKit;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Org.BouncyCastle.Tls;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using FlightAgreementApplication.FAAWrapper;
using Org.BouncyCastle.Crypto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlightAgreementApplication.FAAData
{
    public class FAALoginSignupData
    {

        private readonly FlightAgreementAppContext _context;
        public FAALoginSignupData(FlightAgreementAppContext context)
        {
            _context = context;
        }

        public AuthResponse Login(LoginRequestDto request)
        {
            try
            {
                var user = _context.users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.UserEmail == request.UserEmail);



                if (user == null)
                {
                    return new AuthResponse { Messege = "Invalid email or password." };
                }

                if ((int)user.ActivityStatus == 1)
                {
                    return new AuthResponse { Messege = "Account is not activated." };
                }

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.UserPassword, request.UserPassword);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                {
                    return new AuthResponse { Messege = "Invalid email or password." };
                }

                var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName;

                if (string.IsNullOrEmpty(role))
                {
                    return new AuthResponse { Messege = "User role not found." };
                }
                if (user != null)
                {
                    var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false)
           .Build();

                    var secretKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "Airline",
                        audience: "https://localhost:44355",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    var userDetails = new AuthResponse
                    {
                        Token = tokenString,
                        UserId = user.Id,
                        UserName = user.UserName,
                        UserEmail = user.UserEmail,
                        UserRole = role
                    };

                    switch (role)
                    {
                        case "TourOperator":
                            userDetails.Details = new UserDetails
                            {
                                TourOperatorAddress = _context.UserRole
                                    .Where(ur => ur.UId == userDetails.UserId)
                                    .Select(ur => ur.TourOperator.TourOperatorAddress)
                                    .FirstOrDefault(),
                                TourOperatorPhone = _context.UserRole
                                    .Where(ur => ur.UId == userDetails.UserId)
                                    .Select(ur => ur.TourOperator.TourOperatorPhone)
                                    .FirstOrDefault(),
                                TourOperatorLandLine = _context.UserRole
                                    .Where(ur => ur.UId == userDetails.UserId)
                                    .Select(ur => ur.TourOperator.TourOperatorLandLine)
                                    .FirstOrDefault(),
                                TourOperatorContactPreference = _context.UserRole
                                    .Where(ur => ur.UId == userDetails.UserId)
                                    .Select(ur => ur.TourOperator.TourOperatorContactPreferences)
                                    .FirstOrDefault()
                            };
                            break;

                        case "AirlineManager":
                            userDetails.Details = new UserDetails
                            {
                                AirlineManagerPhone = _context.UserRole
                                    .Where(ur => ur.UId == userDetails.UserId)
                                    .Select(ur => ur.AirlineManager.AirlineManagerPhone)
                                    .FirstOrDefault(),
                                AirlineManagerLandLine = _context.UserRole
                                    .Where(ur => ur.UId == userDetails.UserId)
                                    .Select(ur => ur.AirlineManager.AirlineManagerLandLine)
                                    .FirstOrDefault()
                            };
                            break;

                        default:
                            // Handle other roles as needed
                            break;
                    }


                    return userDetails;
                }
                else
                {
                    return new AuthResponse { Messege = "Invalid email or password." };
                }
            }
            catch (Exception ex)
            {
                return new AuthResponse { Messege = "An error occurred during login." };
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

        public async Task<ActivateAccountResponse> ActivateAccount(ActivationRequestDto request)
        {
            try
            {
                var user = await _context.users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);

                if (user == null)
                {
                    return new ActivateAccountResponse { Messege = "User not found" };
                }

                // Token verification logic here
                if (user.ActivationToken != request.ActivationToken || DateTime.UtcNow > user.ActivationTokenExpiry)
                {
                    return new ActivateAccountResponse { Messege = "Invalid or expired activation token" };
                }

                user.ActivityStatus = IsActive.Active;
                await _context.SaveChangesAsync();

                var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName;

                if (string.IsNullOrEmpty(role))
                {
                    return new ActivateAccountResponse { Messege = "User role not found." };
                }

                var userDetails = new ActivateAccountResponse
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserRole = role,
                    Token = null, 

                    Details = role switch
                    {
                        "TourOperator" => new UserDetails
                        {
                            TourOperatorAddress = _context.UserRole
                                .Where(ur => ur.UId == user.Id)
                                .Select(ur => ur.TourOperator.TourOperatorAddress)
                                .FirstOrDefault(),
                            TourOperatorPhone = _context.UserRole
                                .Where(ur => ur.UId == user.Id)
                                .Select(ur => ur.TourOperator.TourOperatorPhone)
                                .FirstOrDefault(),
                            TourOperatorLandLine = _context.UserRole
                                .Where(ur => ur.UId == user.Id)
                                .Select(ur => ur.TourOperator.TourOperatorLandLine)
                                .FirstOrDefault(),
                            TourOperatorContactPreference = _context.UserRole
                                .Where(ur => ur.UId == user.Id)
                                .Select(ur => ur.TourOperator.TourOperatorContactPreferences)
                                .FirstOrDefault()
                        },
                        "AirlineManager" => new UserDetails
                        {
                            AirlineManagerPhone = _context.UserRole
                                .Where(ur => ur.UId == user.Id)
                                .Select(ur => ur.AirlineManager.AirlineManagerPhone)
                                .FirstOrDefault(),
                            AirlineManagerLandLine = _context.UserRole
                                .Where(ur => ur.UId == user.Id)
                                .Select(ur => ur.AirlineManager.AirlineManagerLandLine)
                                .FirstOrDefault()
                        },
                        _ => null
                    }
                };

                if (userDetails != null)
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();

                    var secretKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "Airline",
                        audience: "https://localhost:44355",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    userDetails.Token = tokenString;
                }

                return userDetails;
            }
            catch (Exception ex)
            {
                return new ActivateAccountResponse { Messege = "An error occurred during activation." };
            }
        }


        private ActivateAccountResponse NotFound(string v)
        {
            throw new NotImplementedException();
        }


       
            public async Task<TourOperatorCreateResponse> CreateTourOperator(TourOperatorDto tourOperatorDto) {

            try
            {
                var tourOperator = new TourOperator
                {
                    TourOperatorId = Guid.NewGuid(),
                    TourOperatorName = tourOperatorDto.TourOperatorName,
                    TourOperatorAddress = tourOperatorDto.TourOperatorAddress,
                    TourOperatorEmail = tourOperatorDto.TourOperatorEmail,
                    TourOperatorPhone = tourOperatorDto.TourOperatorPhone,
                    TourOperatorLandLine = tourOperatorDto.TourOperatorLandLine,
                    TourOperatorContactPreferences = (ContactPreference)tourOperatorDto.TourOperatorContactPreferences,
                    AddedBy = "Admin",
                };

                _context.TourOperators.Add(tourOperator);
                await _context.SaveChangesAsync();

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = tourOperatorDto.TourOperatorName,
                    UserEmail = tourOperatorDto.TourOperatorEmail,
                    //UserPassword = tourOperatorDto.TourOperatorPassword,
                    ActivityStatus = IsActive.Inactive,
                    ActivationToken = GenerateToken(),
                    ActivationTokenExpiry = DateTime.UtcNow.AddHours(24),
                };

                var passwordHasher = new PasswordHasher<User>();
                user.UserPassword = passwordHasher.HashPassword(user, tourOperatorDto.TourOperatorPassword);

                _context.users.Add(user);
                await _context.SaveChangesAsync();

                var tourOperatorRole = _context.Roles.FirstOrDefault(r => r.RoleName == "TourOperator");
                if (tourOperatorRole == null)
                {
                    throw new InvalidOperationException("Role 'TourOperator' not found.");
                }

                var userRole = new UserRole
                {
                    UserRoleId = Guid.NewGuid(),
                    UId = user.Id,
                    RId = tourOperatorRole.Id,
                    TourOperatorId = tourOperator.TourOperatorId,
                };

                _context.UserRole.Add(userRole);
                await _context.SaveChangesAsync();



                var responseData = new TourOperatorCreateResponse
                {
                    TourOperatorId = tourOperator.TourOperatorId,
                    TourOperatorName = tourOperator.TourOperatorName,
                    TourOperatorAddress = tourOperator.TourOperatorAddress,
                    TourOperatorEmail = tourOperator.TourOperatorEmail,
                    TourOperatorPhone = tourOperator.TourOperatorPhone,
                    TourOperatorLandLine = tourOperator.TourOperatorLandLine,
                    TourOperatorContactPreferences = tourOperator.TourOperatorContactPreferences,
                    AddedBy = tourOperator.AddedBy,
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

                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request.", ex);
            }
        }
        public async Task<AirlineManagerCreateResponse> CreateAirlineManager(AirlineManagerRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return new AirlineManagerCreateResponse { Message = "Invalid data received from the client." };
            }

            try
            {
                var airlineManager = new AirlineManager
                {
                    AirlineManagerId = Guid.NewGuid(),
                    AirlineManagerName = requestDto.AirlineManagerName,
                    AirlineManagerEmail = requestDto.AirlineManagerEmail,
                    AirlineManagerPhone = requestDto.AirlineManagerPhone,
                    AirlineManagerLandLine = requestDto.AirlineManagerLandLine,
                    AddedBy = "Admin",
                };

                _context.AirlineManagers.Add(airlineManager);
                await _context.SaveChangesAsync();

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = requestDto.AirlineManagerName,
                    UserEmail = requestDto.AirlineManagerEmail,
                    //UserPassword = requestDto.AirlineManagerPassword,
                    ActivityStatus = IsActive.Inactive,
                    ActivationToken = GenerateToken(),
                    ActivationTokenExpiry = DateTime.UtcNow.AddHours(24),
                };


                var passwordHasher = new PasswordHasher<User>();
                user.UserPassword = passwordHasher.HashPassword(user, requestDto.AirlineManagerPassword);


                _context.users.Add(user);
                await _context.SaveChangesAsync();


                var airlineManagerRole = _context.Roles.FirstOrDefault(r => r.RoleName == "AirlineManager");
                if (airlineManagerRole == null)
                {
                    return new AirlineManagerCreateResponse { Message = "Role 'AirlineManager' not found." };
                }


                var userRole = new UserRole
                {
                    UserRoleId = Guid.NewGuid(),
                    UId = user.Id,
                    RId = airlineManagerRole.Id,
                    AirlineManagerId = airlineManager.AirlineManagerId,
                };

                _context.UserRole.Add(userRole);
                await  _context.SaveChangesAsync();


                var responseData = new AirlineManagerCreateResponse
                {

                    AirlineManagerId = airlineManager.AirlineManagerId,
                    AirlineManagerName = airlineManager.AirlineManagerName,
                    AirlineManagerEmail = airlineManager.AirlineManagerEmail,
                    AirlineManagerPhone = airlineManager.AirlineManagerPhone,
                    AirlineManagerLandLine = airlineManager.AirlineManagerLandLine,
                    AddedBy = airlineManager.AddedBy,
                    UserRoleId = userRole.UserRoleId,
                    UId = userRole.UId,
                    RId = userRole.RId,
                    UserRole = user.UserRoles?.FirstOrDefault()?.Role?.RoleName,
                    //Token = user.ActivationToken
                };
                if (responseData != null)
                {
                    var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json", optional: false)
                       .Build();

                    var secretKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "Airline",
                        audience: "https://localhost:44355",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    responseData.Token= tokenString;
                }

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


                return responseData;
            }
            catch (Exception ex)
            {
                return new AirlineManagerCreateResponse { Message = "Internal server error" };
            }

        }

        public async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordDto request)
        {
            try
            {
                var user = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);
                if (user == null)
                {

                    return new UpdatePasswordResponse { messege = "User not found" };
                }

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.UserPassword, request.CurrentPassword);


                if (passwordVerificationResult != PasswordVerificationResult.Success)
                {

                    return new UpdatePasswordResponse { messege = "Current password is incorrect" };

                }


                user.UserPassword = passwordHasher.HashPassword(user, request.NewPassword);

                await _context.SaveChangesAsync();

                return new UpdatePasswordResponse { messege = "Password updated successfully" };
            }
            catch (Exception ex)
            {

                return new UpdatePasswordResponse { messege = "An error occurred during password update." };

            }
        }

        public async Task<ResetPasswordResponse> ResetPasswordRequest([FromBody] ResetPasswordRequestDto request) {

            try
            {
                var user = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);
                if (user == null)
                {

                    return new ResetPasswordResponse { messege="User not found" };
                }


                user.ResetToken = GenerateToken();
                user.ResetTokenExpiry = DateTime.UtcNow.AddHours(24);
                await _context.SaveChangesAsync();


                var resetPasswordLink = $"http://localhost:3000/login/resetPassword/{user.ResetToken}/?email={user.UserEmail}";


                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("ccchinmaysinnn@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.UserEmail));
                email.Subject = "FAA Account's Forgot Password Link";

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = $"Click the following link to reset your password: {resetPasswordLink}"
                };
                email.Body = bodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("ccchinmaysinnn@gmail.com", "vcxe hoqo qlvp hekw");
                smtp.Send(email);
                smtp.Disconnect(true);


                return new ResetPasswordResponse { messege = "Reset token generated successfully" };
           
            }
            catch (Exception ex)
            {
                return new ResetPasswordResponse { messege = "An error occurred during reset token generation." };               
            }
        }

        public async Task<ResetPasswordVerifyResponse> ResetPasswordVerify( ResetPasswordVerifyRequestDto request)
        {
            try
            {

                var user = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);
                if (user == null)
                {
                 
                    return new ResetPasswordVerifyResponse { messege = "User not found" };
                }


                if (user.ResetToken != request.ResetToken || DateTime.UtcNow > user.ResetTokenExpiry)
                {
          
                    return new ResetPasswordVerifyResponse { messege = "Invalid or expired reset token" };
                }


                var passwordHasher = new PasswordHasher<User>();
                user.UserPassword = passwordHasher.HashPassword(user, request.NewPassword);

                user.ResetToken = null;
                user.ResetTokenExpiry = null;

                await _context.SaveChangesAsync();
                return new ResetPasswordVerifyResponse { messege = "Password reset successful" };
                
            }
            catch (Exception ex)
            {
                return new ResetPasswordVerifyResponse { messege = "An error occurred during password reset." };               
            }
        }

        public async Task<UpdateUserDetailsResponse> UpdateUserDetails( Guid userId, UpdateUserDetailsDto userDetailsDto)
        {
            try
            {

                var user = await _context.users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return new UpdateUserDetailsResponse { messege = $"User with ID {userId} not found." };
                }

                switch (user.UserRoles?.FirstOrDefault()?.Role?.RoleName)
                {
                    case "AirlineManager":

                        var airlineManager = await _context.AirlineManagers
                            .FirstOrDefaultAsync(am => am.AirlineManagerId == user.UserRoles.First().AirlineManagerId);

                        if (airlineManager == null)
                        {
                            return new UpdateUserDetailsResponse { messege = $"AirlineManager not found for user with ID {userId}." };
                            
                        }

                        airlineManager.AirlineManagerName = userDetailsDto.UserName;
                        //airlineManager.AirlineManagerEmail = userDetailsDto.UserEmail;
                        airlineManager.AirlineManagerPhone = userDetailsDto.AirlineManagerPhone;
                        airlineManager.AirlineManagerLandLine = userDetailsDto.AirlineManagerLandLine;

                        airlineManager.ModifiedBy = "user";
                        airlineManager.ModifyDate = DateTime.UtcNow;
                        break;

                    case "TourOperator":

                        var tourOperator = await _context.TourOperators
                            .FirstOrDefaultAsync(to => to.TourOperatorId == user.UserRoles.First().TourOperatorId);

                        if (tourOperator == null)
                        {
                            return new UpdateUserDetailsResponse { messege = $"TourOperator not found for user with ID {userId}." };

                        }

                        tourOperator.TourOperatorName = userDetailsDto.UserName;
                        //tourOperator.TourOperatorEmail = userDetailsDto.UserEmail;
                        tourOperator.TourOperatorPhone = userDetailsDto.TourOperatorPhone;
                        tourOperator.TourOperatorLandLine = userDetailsDto.TourOperatorLandLine;
                        tourOperator.TourOperatorContactPreferences = userDetailsDto.TourOperatorContactPreference;
                        tourOperator.TourOperatorAddress = userDetailsDto.TourOperatorAddress;


                        tourOperator.ModifiedBy = "user";
                        tourOperator.ModifyDate = DateTime.UtcNow;
                        break;
                    default:                        
                        return new UpdateUserDetailsResponse { messege = "Unsupported role for updating user details." };
                }

                user.UserName = userDetailsDto.UserName;
                //user.UserEmail = userDetailsDto.UserEmail;

                _context.users.Update(user);
                await _context.SaveChangesAsync();

                //return Ok("User details updated successfully.");
                var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName;
                var userDetails = new UpdateUserDetailsResponse
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserRole = role,

                };
                switch (role)
                {
                    case "TourOperator":
                        userDetails.Details = new UserDetails
                        {
                            TourOperatorAddress = _context.UserRole
                                .Where(ur => ur.UId == userDetails.UserId)
                                .Select(ur => ur.TourOperator.TourOperatorAddress)
                                .FirstOrDefault(),
                            TourOperatorPhone = _context.UserRole
                                .Where(ur => ur.UId == userDetails.UserId)
                                .Select(ur => ur.TourOperator.TourOperatorPhone)
                                .FirstOrDefault(),
                            TourOperatorLandLine = _context.UserRole
                                .Where(ur => ur.UId == userDetails.UserId)
                                .Select(ur => ur.TourOperator.TourOperatorLandLine)
                                .FirstOrDefault(),
                            TourOperatorContactPreference = _context.UserRole
                                .Where(ur => ur.UId == userDetails.UserId)
                                .Select(ur => ur.TourOperator.TourOperatorContactPreferences)
                                .FirstOrDefault()
                        };
                        break;

                    case "AirlineManager":
                        userDetails.Details = new UserDetails
                        {
                            AirlineManagerPhone = _context.UserRole
                                .Where(ur => ur.UId == userDetails.UserId)
                                .Select(ur => ur.AirlineManager.AirlineManagerPhone)
                                .FirstOrDefault(),
                            AirlineManagerLandLine = _context.UserRole
                                .Where(ur => ur.UId == userDetails.UserId)
                                .Select(ur => ur.AirlineManager.AirlineManagerLandLine)
                                .FirstOrDefault()
                        };
                        break;
                    default:                     
                        break;
                }
                userDetails.messege = "User Updated Sucessfully";
                return userDetails;
            }                         
            catch (Exception ex)
            {
             
                return new UpdateUserDetailsResponse { messege = "An error occurred while processing your request." };
            }

        }

            private AirlineManagerCreateResponse Ok(AirlineManagerCreateResponse responseData)
        {
            throw new NotImplementedException();
        }
    }
}
