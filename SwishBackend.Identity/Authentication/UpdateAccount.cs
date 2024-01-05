using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwishBackend.Identity.Data;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SwishBackend.Identity.Authentication
{
    public class UpdateAccount : IUpdateAccount
    {

        private ApiResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly string secretKey;
        private readonly IConfiguration _configuration;

        public UpdateAccount(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IConfiguration configuration
            )
        {
            _response = new ApiResponse();
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public async Task<ApiResponse> UpdateEmail([FromBody] UpdateEmailRequestDTO email)
        {
            if (email.NewEmail != null || email.UserId != null)
            {
                var applicationUser = _context.ApplicationUsers.Where(x => x.Id == email.UserId).FirstOrDefault();
                if (applicationUser != null)
                {

                    //TODO: switch to rabbitMQ to handle email and map to correct object.

                    var setEmail = await _userManager.SetEmailAsync(applicationUser, email.NewEmail);
                    applicationUser.UserName = email.NewEmail;
                    applicationUser.NormalizedEmail = email.NewEmail.ToUpper();
                    var save = await _context.SaveChangesAsync();
                    if (setEmail.Succeeded)
                    {
                        string token = await RefreshAuthToken(applicationUser);
                        ResponseResult(HttpStatusCode.OK, true, "", token);
                        return _response;
                    }
                    else
                    {
                        ResponseResult(HttpStatusCode.Conflict, false, "Unable to update email", "");
                        return _response;
                    }
                }
                ResponseResult(HttpStatusCode.NotFound, false, $"No user with id: {email.UserId} was found", "");
                return _response;
            }
            ResponseResult(HttpStatusCode.BadRequest, false, "One or more fields were missing", "");
            return _response;
        }



        public async Task<ApiResponse> UpdateName([FromBody] UpdateNameRequestDTO name)
        {
            if (name.NewName != null || name.OldName != null || name.UserId != null)
            {
                var applicationUser = _context.ApplicationUsers.Where(x => x.Id == name.UserId).FirstOrDefault();
                if (applicationUser != null)
                {
                    applicationUser.Name = name.NewName;
                    var save = await _context.SaveChangesAsync();
                    if (save == 1)
                    {
                        //we have to generate jwt token
                        string Token = await RefreshAuthToken(applicationUser);
                        //set response
                        ResponseResult(HttpStatusCode.OK, true, "", Token);
                        return _response;
                    }
                    else
                    {
                        ResponseResult(HttpStatusCode.Conflict, false, "Unable to update the same item to the database, use a unique name", "");
                        return _response;
                    }
                }
                ResponseResult(HttpStatusCode.NotFound, false, $"No user with id: {name.UserId} was found", "");
                return _response;
            }
            ResponseResult(HttpStatusCode.BadRequest, false, "One or more fields were missing!", "");
            return _response;
        }

        public async Task<ApiResponse> UpdatePassword([FromBody] UpdatePasswordRequestDTO password)
        {
            if (InvalidPasswordFields(password))
            {
                ResponseResult(HttpStatusCode.BadRequest, false, "Some fields were missing", "");
                return _response;
            }

            if (password.NewPassword != password.RepeatedPassword)
            {
                ResponseResult(HttpStatusCode.BadRequest, false, "New and repeated passwords do not match", "");
                return _response;
            }

            var applicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.Id == password.UserId);
            if (applicationUser == null)
            {
                ResponseResult(HttpStatusCode.NotFound, false, "User was not found", "");
                return _response;
            }

            var setPassword = await _userManager.ChangePasswordAsync(applicationUser, password.OldPassword, password.NewPassword);
            return setPassword.Succeeded
                ? OkResponse(await RefreshAuthToken(applicationUser))
            : StatusCodeResponse("The server was unable to update the password");
        }

        private void ResponseResult(HttpStatusCode statusCode, bool successResult, string errorMessage, object apiResult)
        {
            _response.StatusCode = statusCode;
            _response.IsSuccess = successResult;
            _response.ErrorMessages.Add(errorMessage);
            _response.Result = apiResult;
        }

        private ApiResponse OkResponse(string token)
        {
            ResponseResult(HttpStatusCode.OK, true, "", token);
            return _response;
        }

        private ApiResponse StatusCodeResponse(string message)
        {
            ResponseResult(HttpStatusCode.InternalServerError, false, message, "");
            return _response;
        }

        private async Task<string> RefreshAuthToken(ApplicationUser? applicationUser)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            var roles = await _userManager.GetRolesAsync(applicationUser);
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            //add claims to the token
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                                new Claim("fullName", applicationUser.Name),
                                new Claim("id", applicationUser.Id.ToString()),
                                new Claim(ClaimTypes.Email, applicationUser.UserName.ToString()),
                                new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                //set signin credentials
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            //here the actual token is generated:
            //populates the token with the claims
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var Token = tokenHandler.WriteToken(token);
            return Token;
        }
        private bool InvalidPasswordFields(UpdatePasswordRequestDTO password)
        {
            return password.UserId == null ||
                password.OldPassword == null ||
                password.RepeatedPassword == null ||
                password.NewPassword == null;
        }
    }
}
