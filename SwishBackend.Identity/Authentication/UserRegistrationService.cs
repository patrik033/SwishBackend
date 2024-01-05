using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Email.Register;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;
using System.Net;
using MassTransit;
using AutoMapper;
using MassTransitCommons.Common.Email;


namespace SwishBackend.Identity.Authentication
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRegistrationService> _logger;
        private readonly ISendGridEmailRegister _sendGridEmailRegister;
        private ApiResponse _response;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public UserRegistrationService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserRegistrationService> logger,
            ISendGridEmailRegister sendGridEmailRegister,
            IPublishEndpoint publishEndpoint,
            IMapper mapper)
        {
            _response = new ApiResponse();
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _sendGridEmailRegister = sendGridEmailRegister;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<ApiResponse> RegisterUserAsync(RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                // Check if the username is already taken
                var existingUser = await _userManager.FindByEmailAsync(registerRequestDTO.Email);
                if (existingUser != null)
                {
                    ResponseResult(HttpStatusCode.BadRequest, false, "Username already exists.", "");
                    return _response;
                }

                // Create a new user
                var newUser = new ApplicationUser
                {
                    Name = registerRequestDTO.Email,
                    UserName = registerRequestDTO.Email,
                    Email = registerRequestDTO.Email,
                    NormalizedEmail = registerRequestDTO.Email.ToUpper(),
                    NormalizedUserName = registerRequestDTO.Email,
                };

                var result = await _userManager.CreateAsync(newUser, registerRequestDTO.Password);

                if (result.Succeeded)
                {
                    await EnsureRolesExistAsync();

                    var userRole = GetUserRole(registerRequestDTO.Role);
                    await _userManager.AddToRoleAsync(newUser, userRole);
                    
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var urlToken = Base64UrlEncoder.Encode(emailToken);
                    var token = $"http://localhost:5173/RegisterConfirmation?email={newUser.Email}&activationToken={urlToken}";
                   
                    var emailConfirmationMessage = _mapper.Map<EmailConfirmationMessage>(newUser);
                    emailConfirmationMessage.UserId = newUser.Id;
                    emailConfirmationMessage.EmailToken = token;
                    await _publishEndpoint.Publish(emailConfirmationMessage);

                    ResponseResult(HttpStatusCode.OK,true,"","Please check your email for the confirmition email.");
                    return _response;
                }

                else
                {
                    ResponseResult(HttpStatusCode.BadRequest, false, result.Errors.FirstOrDefault()?.Description, null);
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception was thrown during user registration: {ex.Message}", ex);
                var errorMessage = "Internal Server Error";
                //exceptionresult
                ResponseResult(HttpStatusCode.InternalServerError, false, errorMessage, "");
                return _response;
            }
        }


        private void ResponseResult(HttpStatusCode statusCode, bool successResult, string errorMessage, object apiResult)
        {
            _response.StatusCode = statusCode;
            _response.IsSuccess = successResult;
            _response.ErrorMessages.Add(errorMessage);
            _response.Result = apiResult;
        }


        private async Task EnsureRolesExistAsync()
        {
            if (!await _roleManager.RoleExistsAsync(StaticDetails.Role_Admin))
            {
                // Create roles in the database
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin));
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_User));
            }
        }

        private string GetUserRole(string requestedRole)
        {
            return requestedRole.ToLower() == StaticDetails.Role_Admin.ToLower()
                ? StaticDetails.Role_Admin
                : StaticDetails.Role_User;
        }

        private async Task<SendGrid.Response> SendConfirmationEmail(ApplicationUser newUser)
        {
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var urlToken = Base64UrlEncoder.Encode(emailToken);
            var token = $"http://localhost:3000/RegisterConfirmation?email={newUser.Email}&activationToken={urlToken}";
            string body = $"<a href='{token}'>Klicka på länken för att bekräfta</a>";
            var emailSettings = Environment.GetEnvironmentVariable("EmailSettings");

            var result = await _sendGridEmailRegister.SendAsync(emailSettings, emailSettings, "Confirm your email", token, newUser.Name);

            return result;
        }
    }
}
