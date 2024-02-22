using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwishBackend.Identity.Email.Token;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;
using System.Net;

namespace SwishBackend.Identity.Authentication
{
    public class ResetService : IResetService
    {

        private ApiResponse _response;
        private readonly  UserManager<ApplicationUser> _userManager;
        private readonly ISendGridEmailTokens _sendGridEmailTokens;
        public ResetService(UserManager<ApplicationUser> userManager,ISendGridEmailTokens sendGridEmailTokens)
        {
            _response = new ApiResponse();
            _userManager = userManager;
            _sendGridEmailTokens = sendGridEmailTokens;
        }

        public async Task<ApiResponse> ConfirmPasswordReset([FromQuery] string email, [FromQuery] string password, [FromQuery] string activationToken)
        {
            if (email.IsNullOrEmpty() || password.IsNullOrEmpty() || activationToken.IsNullOrEmpty())
            {
                ResponseResult(HttpStatusCode.BadRequest, false, "confirmed and new password doesn't match or email is empty", "");
                return _response;
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, Base64UrlEncoder.Decode(activationToken), password);

                if (result.Succeeded)
                {
                    ResponseResult(HttpStatusCode.OK, true, "", result);
                    return _response;
                }
                else
                {
                    var identityDescribor = new IdentityErrorDescriber().InvalidToken();
                    var expiredToken = result.Errors.Any(x => x.Code == nameof(IdentityErrorDescriber.InvalidToken));
                    if (expiredToken)
                    {
                        ResponseResult(HttpStatusCode.Forbidden, false, "The token has expired", "");
                        return _response;
                    }
                }
            }
            ResponseResult(HttpStatusCode.InternalServerError, false, "An error occurred while confirmation your email address.", "");
            return _response;
        }

        public async Task<ApiResponse> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user != null)
            {
                var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string token = $"http://localhost:3000/UpdatePassword?email={user.Email}&activationToken={Base64UrlEncoder.Encode(passwordToken)}";
                string body = $"<a href='{token}'>Klicka på länken för att bekräfta</a>";

                var emailSettings = Environment.GetEnvironmentVariable("EmailSettings");




                await _sendGridEmailTokens.SendAsync(emailSettings, emailSettings, "Reset Password link", token, user.Name);
                ResponseResult(HttpStatusCode.OK, true, "Password reset was successfull", "");
                return _response;
            }
            ResponseResult(HttpStatusCode.Unauthorized, false, "", "");
            return _response;
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
