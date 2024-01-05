using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwishBackend.Identity.Data;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SwishBackend.Identity.Authentication
{
    public class UserLoginService : IUserLoginService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _secretKey;
        private readonly IConfiguration _configuration;
        private readonly ApiResponse _response;
        public UserLoginService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _secretKey = _configuration.GetValue<string>("ApiSettings:Secret");
            _response = new ApiResponse();
        }

        public async Task<ApiResponse> LoginUserAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isValid)
            {
                ResponseResult(HttpStatusCode.Unauthorized,false, "Username or password was incorrect","");
                return _response;
            }

            bool isConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (!isConfirmed)
            {
                ResponseResult(HttpStatusCode.Forbidden, false, "You must confirm your account before you login!", "");
                return _response;
            }

            // Generate JWT token
            JwtSecurityTokenHandler tokenHandler = new();
            var roles = await _userManager.GetRolesAsync(user);
            byte[] key = Encoding.ASCII.GetBytes(_secretKey);

            // Add claims to the token
            SecurityTokenDescriptor tokenDescriptor = AddClaimsToToken(user, roles, key);

            // Generate the actual token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var loginResponse = new LoginResponseDTO
            {
                Email = user.Email,
                Token = tokenHandler.WriteToken(token),
            };

            if (string.IsNullOrEmpty(loginResponse.Email) || string.IsNullOrEmpty(loginResponse.Token))
            {
                ResponseResult(HttpStatusCode.BadRequest, false, "Username or password is incorrect", "");
                return _response;
            }

            var unathorizedResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Username or password was incorrect" },
            };

            ResponseResult(HttpStatusCode.OK, true, "", loginResponse);
            return _response;
        }

        private static SecurityTokenDescriptor AddClaimsToToken(ApplicationUser? user, IList<string> roles, byte[] key)
        {
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName", user.Name),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                // Set signin credentials
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            return tokenDescriptor;
        }


        private void ResponseResult(HttpStatusCode statusCode, bool successResult, string errorMessage, object apiResult)
        {
            _response.StatusCode = statusCode;
            _response.IsSuccess = successResult;
            _response.ErrorMessages.Add(errorMessage);
            _response.Result = apiResult;
        }

    }
}
