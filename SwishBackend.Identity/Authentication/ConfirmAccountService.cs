using AutoMapper;
using MassTransit;
using MassTransitCommons.Common.Email;
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
    public class ConfirmAccountService : IConfirmAccountService
    {

        private ApiResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendGridEmailTokens _sendGridEmailTokens;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ConfirmAccountService(
            UserManager<ApplicationUser> userManager,
            ISendGridEmailTokens sendGridEmailTokens,
            IMapper mapper,
            IPublishEndpoint publishEndpoint
            )
        {
            _response = new ApiResponse();
            _userManager = userManager;
            _sendGridEmailTokens = sendGridEmailTokens;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }



        public async Task<ApiResponse> ConfirmEmail([FromQuery] string email, [FromQuery] string activationToken)
        {
            if (email.IsNullOrEmpty() || activationToken.IsNullOrEmpty())
            {
                ResponseResult(HttpStatusCode.BadRequest, false, "Either email or verification code is invalid.", "");
                return _response;
            }

            var user = await _userManager.FindByNameAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, Base64UrlEncoder.Decode(activationToken));

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
        public async Task<ApiResponse> ResendEmailActivationLink([FromBody] ResendRequestAgain email)
        {
            var user = await _userManager.FindByNameAsync(email.Email);
            if (user != null)
            {

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var urlToken = Base64UrlEncoder.Encode(emailToken);
                string token = string.Format($"http://localhost:5173/RegisterConfirmation?email={user.Email}&activationToken={urlToken}");

                var emailConfirmationMessage = _mapper.Map<EmailConfirmationMessage>(user);
                emailConfirmationMessage.UserId = user.Id;
                emailConfirmationMessage.EmailToken = token;
                await _publishEndpoint.Publish(emailConfirmationMessage);

                ResponseResult(HttpStatusCode.OK, true, "", "Please check your email for the confirmition email.");
                return _response;
            }
            ResponseResult(HttpStatusCode.Unauthorized, false, "Invalid User", "");
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
