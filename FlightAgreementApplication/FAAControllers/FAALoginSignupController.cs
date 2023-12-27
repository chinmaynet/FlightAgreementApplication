using FlightAgreementApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FlightAgreementApplication.DTO.Request;
using FlightAgreementApplication.FAAWrapper;
using Org.BouncyCastle.Crypto;
using FlightAgreementApplication.DTO.Response;

namespace FlightAgreementApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FAALoginSignupController : ControllerBase
    {

        private readonly FAALoginSignupWrapper _wrapper;
        private readonly FlightAgreementAppContext _context;
        public FAALoginSignupController(FAALoginSignupWrapper wrapper, FlightAgreementAppContext context)
        {
            _wrapper = wrapper;
            //_context = context;
        }

        //public FAALoginSignupController(FlightAgreementAppContext context)
        //{
        //    _context = context;
        //}

        /*
        [HttpPost("CreateAirlineManager")]
        public async Task<IActionResult> CreateAirlineManager([FromBody] AirlineManagerRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Invalid data received from the client.");
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
                    return NotFound("Role 'AirlineManager' not found.");
                }

                var userRole = new UserRole
                {
                    UserRoleId = Guid.NewGuid(),
                    UId = user.Id,
                    RId = airlineManagerRole.Id,
                    AirlineManagerId = airlineManager.AirlineManagerId,
                };

                _context.UserRole.Add(userRole);
                await _context.SaveChangesAsync();


                var responseData = new
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


                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        */

        [HttpPost("CreateAirlineManager")]
        public async Task<AirlineManagerCreateResponse> CreateAirlineManager([FromBody] AirlineManagerRequestDto requestDto)
        {
            AirlineManagerCreateResponse airlineManagerCreateResponse= await _wrapper.CreateAirlineManager(requestDto);

                return airlineManagerCreateResponse;
        }



        [HttpPost("CreateTourOperator")]

        public async Task<TourOperatorCreateResponse> CreateTourOperator([FromBody] TourOperatorDto tourOperatorDto) {

            TourOperatorCreateResponse tourOperatorCreateResponse =await _wrapper.CreateTourOperator(tourOperatorDto);

            return tourOperatorCreateResponse;
        }
       

        //public async Task<IActionResult> CreateTourOperator([FromBody] TourOperatorDto tourOperatorDto)
        //{
        //    try
        //    {
        //        var tourOperator = new TourOperator
        //        {
        //            TourOperatorId = Guid.NewGuid(),
        //            TourOperatorName = tourOperatorDto.TourOperatorName,
        //            TourOperatorAddress = tourOperatorDto.TourOperatorAddress,
        //            TourOperatorEmail = tourOperatorDto.TourOperatorEmail,
        //            TourOperatorPhone = tourOperatorDto.TourOperatorPhone,
        //            TourOperatorLandLine = tourOperatorDto.TourOperatorLandLine,
        //            TourOperatorContactPreferences = (ContactPreference)tourOperatorDto.TourOperatorContactPreferences,
        //            AddedBy = "Admin",
        //        };

        //        _context.TourOperators.Add(tourOperator);
        //        await _context.SaveChangesAsync();

        //        var user = new User
        //        {
        //            Id = Guid.NewGuid(),
        //            UserName = tourOperatorDto.TourOperatorName,
        //            UserEmail = tourOperatorDto.TourOperatorEmail,
        //            //UserPassword = tourOperatorDto.TourOperatorPassword,
        //            ActivityStatus = IsActive.Inactive,
        //            ActivationToken = GenerateToken(),
        //            ActivationTokenExpiry = DateTime.UtcNow.AddHours(24),
        //        };

        //        var passwordHasher = new PasswordHasher<User>();
        //        user.UserPassword = passwordHasher.HashPassword(user, tourOperatorDto.TourOperatorPassword);

        //        _context.users.Add(user);
        //        await _context.SaveChangesAsync();

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
        //            TourOperatorId = tourOperator.TourOperatorId,
        //        };

        //        _context.UserRole.Add(userRole);
        //        await _context.SaveChangesAsync();



        //        var responseData = new
        //        {
        //            TourOperatorId = tourOperator.TourOperatorId,
        //            TourOperatorName = tourOperator.TourOperatorName,
        //            TourOperatorAddress = tourOperator.TourOperatorAddress,
        //            TourOperatorEmail = tourOperator.TourOperatorEmail,
        //            TourOperatorPhone = tourOperator.TourOperatorPhone,
        //            TourOperatorLandLine = tourOperator.TourOperatorLandLine,
        //            TourOperatorContactPreferences = tourOperator.TourOperatorContactPreferences,
        //            AddedBy = tourOperator.AddedBy,
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

        //        return Ok(responseData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

        //private string GenerateToken()
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(key);
        //    }

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //    new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(24),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        //{
        //    try
        //    {
        //        var user = await _context.users
        //            .Include(u => u.UserRoles)
        //                .ThenInclude(ur => ur.Role)
        //            .FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail
        //            //&& u.UserPassword == request.UserPassword
        //            );

        //        if (user == null)
        //        {
        //            return Unauthorized(new { Message = "Invalid email or password." });
        //        }



        //        if ((int)user.ActivityStatus == 1)
        //        {
        //            return Unauthorized(new { Message = "Account is not activated." });
        //        }

        //        var passwordHasher = new PasswordHasher<User>();
        //        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.UserPassword, request.UserPassword);

        //        if (passwordVerificationResult != PasswordVerificationResult.Success)
        //        {
        //            return Unauthorized(new { Message = "Invalid email or password." });
        //        }

        //        var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName;

        //        if (string.IsNullOrEmpty(role))
        //        {
        //            return BadRequest(new { Message = "User role not found." });
        //        }
        //        if (user != null)
        //        {
        //            var config = new ConfigurationBuilder()
        //   .AddJsonFile("appsettings.json", optional: false)
        //   .Build();

        //            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value));
        //            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //            var tokenOptions = new JwtSecurityToken(
        //                issuer: "Airline",
        //                audience: "https://localhost:44355",
        //                claims: new List<Claim>(),
        //                expires: DateTime.Now.AddMinutes(5),
        //                signingCredentials: signinCredentials
        //            );
        //            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //            var userDetails = new
        //            {
        //                Token = tokenString,
        //                UserId = user.Id,
        //                UserName = user.UserName,
        //                UserEmail = user.UserEmail,
        //                UserRole = role,
        //                Details = role switch
        //                {
        //                    "TourOperator" => (object)await _context.UserRole
        //                        .Where(ur => ur.UId == user.Id)
        //                        .Select(ur => ur.TourOperator)
        //                        .Select(t => new
        //                        {
        //                            TourOperatorAddress = t.TourOperatorAddress,
        //                            TourOperatorPhone = t.TourOperatorPhone,
        //                            TourOperatorLandLine = t.TourOperatorLandLine,
        //                            TourOperatorContactPreferences = t.TourOperatorContactPreferences,
        //                        })
        //                        .FirstOrDefaultAsync(),
        //                    "AirlineManager" => await _context.UserRole
        //                        .Where(ur => ur.UId == user.Id)
        //                        .Select(ur => ur.AirlineManager)
        //                        .Select(t => new
        //                        {
        //                            AirlineManagerPhone = t.AirlineManagerPhone,
        //                            AirlineManagerLandLine = t.AirlineManagerLandLine,
        //                        })
        //                        .FirstOrDefaultAsync(),
        //                    _ => null
        //                }
        //            };
        //            return Ok(userDetails);
        //        }
        //        else
        //        {
        //            return Unauthorized(new { Message = "Invalid email or password." });
        //        }


        //        //return Ok(userDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred during login.", Error = ex.Message });
        //    }
        //}


        [HttpPost("Login")]
        public IActionResult LoginAction([FromBody] LoginRequestDto request)
        {

            AuthResponse loginResponse= _wrapper.Login(request);
            if (loginResponse.Messege == "Invalid email or password.") {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
            else if (loginResponse.Messege == "Account is not activated.") {
                return Unauthorized(new { Message = "Account is not activated." });

            }                            

            return Ok(loginResponse);

        }

        [HttpPost("ActivateAccount")]
        public async Task<ActivateAccountResponse> ActivateAccount([FromBody] ActivationRequestDto request)
        {
            ActivateAccountResponse activateAccountResponse = await  _wrapper.ActivateAccount(request);

            return activateAccountResponse;

        }

        //[HttpPost("ActivateAccount")]
        //public async Task<IActionResult> ActivateAccount([FromBody] ActivationRequestDto request)
        //{
        //    try
        //    {

        //        var user = await _context.users.Include(u => u.UserRoles)
        //                .ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);

        //        if (user == null)
        //        {
        //            return NotFound("User not found");
        //        }

        //        // Perform token verification logic here
        //        if (user.ActivationToken != request.ActivationToken || DateTime.UtcNow > user.ActivationTokenExpiry)
        //        {
        //            return BadRequest("Invalid or expired activation token");
        //        }

        //        user.ActivityStatus = IsActive.Active;
        //        await _context.SaveChangesAsync();

        //        //user.ActivationToken = null;
        //        //user.ActivationTokenExpiry = null;

        //        var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName;

        //        if (string.IsNullOrEmpty(role))
        //        {
        //            return BadRequest(new { Message = "User role not found." });
        //        }

        //        var userDetails = new
        //        {
        //            Token = '',
        //            UserId = user.Id,
        //            UserName = user.UserName,
        //            UserEmail = user.UserEmail,
        //            UserRole = role,
        //            Details = role switch
        //            {
        //                "TourOperator" => (object)await _context.UserRole
        //                    .Where(ur => ur.UId == user.Id)
        //                    .Select(ur => ur.TourOperator)
        //                    .Select(t => new
        //                    {
        //                        TourOperatorAddress = t.TourOperatorAddress,
        //                        TourOperatorPhone = t.TourOperatorPhone,
        //                        TourOperatorLandLine = t.TourOperatorLandLine,
        //                        TourOperatorContactPreferences = t.TourOperatorContactPreferences,
        //                    })
        //                    .FirstOrDefaultAsync(),
        //                "AirlineManager" => await _context.UserRole
        //                    .Where(ur => ur.UId == user.Id)
        //                    .Select(ur => ur.AirlineManager)
        //                    .Select(t => new
        //                    {
        //                        AirlineManagerPhone = t.AirlineManagerPhone,
        //                        AirlineManagerLandLine = t.AirlineManagerLandLine,
        //                    })
        //                    .FirstOrDefaultAsync(),
        //                _ => null
        //            }
        //        };

        //        if (userDetails != null) 
        //        {
        //            var config = new ConfigurationBuilder()
        //                   .AddJsonFile("appsettings.json", optional: false)
        //                   .Build();

        //            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value));
        //            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //            var tokenOptions = new JwtSecurityToken(
        //                issuer: "Airline",
        //                audience: "https://localhost:44355",
        //                claims: new List<Claim>(),
        //                expires: DateTime.Now.AddMinutes(5),
        //                signingCredentials: signinCredentials
        //            );
        //            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //            userDetails.Token= tokenString;

        //        }

        //        return Ok(userDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred during activation.", Error = ex.Message });
        //    }
        //}


        [HttpPost("ResetPasswordRequest")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestDto request)
        {
            try
            {
                ResetPasswordResponse resetPasswordResponse = await _wrapper.ResetPasswordRequest(request);

                if (resetPasswordResponse.messege == "Reset token generated successfully")
                {
                    return Ok("Reset token generated successfully");
                }

                else {
                    return BadRequest(new { Message = "An error occurred during reset token generation." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during reset token generation.", Error = ex.Message });
            }
        }

        //    [HttpPost("ResetPasswordRequest")]
        //public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestDto request)
        //{
        //    try
        //    {
        //        var user = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);
        //        if (user == null)
        //        {
        //            return NotFound("User not found");
        //        }


        //        user.ResetToken = GenerateToken();
        //        user.ResetTokenExpiry = DateTime.UtcNow.AddHours(24);
        //        await _context.SaveChangesAsync();


        //        var resetPasswordLink = $"http://localhost:3000/login/resetPassword/{user.ResetToken}/?email={user.UserEmail}";


        //        var email = new MimeMessage();
        //        email.From.Add(MailboxAddress.Parse("ccchinmaysinnn@gmail.com"));
        //        email.To.Add(MailboxAddress.Parse(user.UserEmail));
        //        email.Subject = "FAA Account's Forgot Password Link";

        //        var bodyBuilder = new BodyBuilder
        //        {
        //            TextBody = $"Click the following link to reset your password: {resetPasswordLink}"
        //        };
        //        email.Body = bodyBuilder.ToMessageBody();

        //        using var smtp = new SmtpClient();
        //        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        //        smtp.Authenticate("ccchinmaysinnn@gmail.com", "vcxe hoqo qlvp hekw");
        //        smtp.Send(email);
        //        smtp.Disconnect(true);



        //        return Ok("Reset token generated successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred during reset token generation.", Error = ex.Message });
        //    }
        //}

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordVerify([FromBody] ResetPasswordVerifyRequestDto request)
        {
            try
            {
                ResetPasswordVerifyResponse ResetPasswordVerifyResponse = await _wrapper.ResetPasswordVerify(request); ;
                if (ResetPasswordVerifyResponse.messege == "Password reset successful")
                {
                    return Ok("Password reset successful");
                }

                else if (ResetPasswordVerifyResponse.messege == "Invalid or expired reset token")
                {
                    return BadRequest(new { Message = "Invalid or expired reset token" });
                }

                else {
                    return NotFound(new  { Message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during password reset.", Error = ex.Message });
            }
        }
            //public async Task<IActionResult> ResetPasswordVerify([FromBody] ResetPasswordVerifyRequestDto request)
            //{
            //    try
            //    {

            //        var user = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);
            //        if (user == null)
            //        {
            //            return NotFound("User not found");
            //        }


            //        if (user.ResetToken != request.ResetToken || DateTime.UtcNow > user.ResetTokenExpiry)
            //        {
            //            return BadRequest("Invalid or expired reset token");
            //        }


            //        var passwordHasher = new PasswordHasher<User>();
            //        user.UserPassword = passwordHasher.HashPassword(user, request.NewPassword);

            //        user.ResetToken = null;
            //        user.ResetTokenExpiry = null;

            //        await _context.SaveChangesAsync();

            //        return Ok("Password reset successful");
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, new { Message = "An error occurred during password reset.", Error = ex.Message });
            //    }
            //}


        [Authorize]
        [HttpPost("UpdatePassword")]

        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto request)
        {
            try
            {
                var updatePasswordResponse = await _wrapper.UpdatePassword(request);

                if (updatePasswordResponse.messege == "Password updated successfully")
                {
                    return Ok("Password updated successfully");
                }
                else
                {
                    return BadRequest(new { Message = "Current password is incorrect" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during password update.", Error = ex.Message });
            }
        }

        //public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto request)
        //{
        //    try
        //    {
        //        var user = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);
        //        if (user == null)
        //        {
        //            return NotFound(new { Message = "User not found" });
        //            return NotFound("User not found");
        //        }

        //        var passwordHasher = new PasswordHasher<User>();
        //        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.UserPassword, request.CurrentPassword);


        //        if (passwordVerificationResult != PasswordVerificationResult.Success)
        //        {
        //            return BadRequest(new { Message = "Current password is incorrect" });

        //        }


        //        user.UserPassword = passwordHasher.HashPassword(user, request.NewPassword);

        //        await _context.SaveChangesAsync();
        //        return Ok("Password updated successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred during password update.", Error = ex.Message });
        //    }
        //}


        [Authorize]
        [HttpPut("UpdateUserDetails/{userId}")]

        public async Task<IActionResult> UpdateUserDetails([FromRoute] Guid userId, [FromBody] UpdateUserDetailsDto userDetailsDto) {

            try
            {
                var UpdateUserDetailsResponse = await _wrapper.UpdateUserDetails(userId, userDetailsDto);

                if (UpdateUserDetailsResponse!=null && UpdateUserDetailsResponse.messege == "User Updated Sucessfully")
                {
                    return Ok(UpdateUserDetailsResponse);
                }
                else
                {
                    return BadRequest(UpdateUserDetailsResponse.messege);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
            //public async Task<IActionResult> UpdateUserDetails([FromRoute] Guid userId, [FromBody] UpdateUserDetailsDto userDetailsDto)
            //{
            //    try
            //    {

            //        var user = await _context.users
            //            .Include(u => u.UserRoles)
            //            .ThenInclude(ur => ur.Role)
            //            .FirstOrDefaultAsync(u => u.Id == userId);

            //        if (user == null)
            //        {
            //            return NotFound($"User with ID {userId} not found.");
            //        }

            //        switch (user.UserRoles?.FirstOrDefault()?.Role?.RoleName)
            //        {
            //            case "AirlineManager":

            //                var airlineManager = await _context.AirlineManagers
            //                    .FirstOrDefaultAsync(am => am.AirlineManagerId == user.UserRoles.First().AirlineManagerId);

            //                if (airlineManager == null)
            //                {
            //                    return NotFound($"AirlineManager not found for user with ID {userId}.");
            //                }

            //                airlineManager.AirlineManagerName = userDetailsDto.UserName;
            //                //airlineManager.AirlineManagerEmail = userDetailsDto.UserEmail;
            //                airlineManager.AirlineManagerPhone = userDetailsDto.AirlineManagerPhone;
            //                airlineManager.AirlineManagerLandLine = userDetailsDto.AirlineManagerLandLine;

            //                airlineManager.ModifiedBy = "user";
            //                airlineManager.ModifyDate = DateTime.UtcNow;
            //                break;

            //            case "TourOperator":

            //                var tourOperator = await _context.TourOperators
            //                    .FirstOrDefaultAsync(to => to.TourOperatorId == user.UserRoles.First().TourOperatorId);

            //                if (tourOperator == null)
            //                {
            //                    return NotFound($"TourOperator not found for user with ID {userId}.");
            //                }

            //                tourOperator.TourOperatorName = userDetailsDto.UserName;
            //                //tourOperator.TourOperatorEmail = userDetailsDto.UserEmail;
            //                tourOperator.TourOperatorPhone = userDetailsDto.TourOperatorPhone;
            //                tourOperator.TourOperatorLandLine = userDetailsDto.TourOperatorLandLine;
            //                tourOperator.TourOperatorContactPreferences = userDetailsDto.TourOperatorContactPreference;
            //                tourOperator.TourOperatorAddress = userDetailsDto.TourOperatorAddress;


            //                tourOperator.ModifiedBy = "user";
            //                tourOperator.ModifyDate = DateTime.UtcNow;
            //                break;
            //            default:
            //                return BadRequest("Unsupported role for updating user details.");
            //        }

            //        user.UserName = userDetailsDto.UserName;
            //        //user.UserEmail = userDetailsDto.UserEmail;

            //        _context.users.Update(user);
            //        await _context.SaveChangesAsync();

            //        //return Ok("User details updated successfully.");
            //        var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName;
            //        var userDetails = new
            //        {
            //            UserId = user.Id,
            //            UserName = user.UserName,
            //            UserEmail = user.UserEmail,
            //            UserRole = role,
            //            Details = role switch
            //            {
            //                "TourOperator" => (object)await _context.UserRole
            //                    .Where(ur => ur.UId == user.Id)
            //                    .Select(ur => ur.TourOperator)
            //                    .Select(t => new
            //                    {
            //                        TourOperatorAddress = t.TourOperatorAddress,
            //                        TourOperatorPhone = t.TourOperatorPhone,
            //                        TourOperatorLandLine = t.TourOperatorLandLine,
            //                        TourOperatorContactPreferences = t.TourOperatorContactPreferences,
            //                    })
            //                    .FirstOrDefaultAsync(),
            //                "AirlineManager" => await _context.UserRole
            //                    .Where(ur => ur.UId == user.Id)
            //                    .Select(ur => ur.AirlineManager)
            //                    .Select(t => new
            //                    {
            //                        AirlineManagerPhone = t.AirlineManagerPhone,
            //                        AirlineManagerLandLine = t.AirlineManagerLandLine,
            //                    })
            //                    .FirstOrDefaultAsync(),
            //                _ => null
            //            }
            //        };

            //        return Ok(userDetails);
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, "An error occurred while processing your request.");
            //    }
            //}
        }
}
