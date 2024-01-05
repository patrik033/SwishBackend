using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Authentication;
using SwishBackend.Identity.Data;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<Authentication> _logger;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserLoginService _userLoginService;
        private readonly IConfirmAccountService _confirmAccountService;
        private readonly IUpdateAccount _updateAccount;
        private readonly IResetService _resetService;
        private readonly IConfiguration _configuration;
        public Authentication
            (
            UserManager<ApplicationUser> userManager,
            ILogger<Authentication> logger,
            IUserRegistrationService userRegistrationService,
            IUserLoginService userLoginService,
            IConfirmAccountService confirmAccountService,
            IUpdateAccount updateAccount,
            IResetService resetService,
            IConfiguration configuration
            )
        {
            _userRegistrationService = userRegistrationService;
            _userLoginService = userLoginService;
            _confirmAccountService = confirmAccountService;
            _updateAccount = updateAccount;
            _resetService = resetService;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;

        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        [AllowAnonymous]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var result = await _userRegistrationService.RegisterUserAsync(registerRequestDTO);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 403)]
        [ProducesResponseType(typeof(ApiResponse), 200)]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var result = await _userLoginService.LoginUserAsync(loginRequestDTO);
            return StatusCode((int)result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("confirm")]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 403)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string activationToken)
        {
            var result = await _confirmAccountService.ConfirmEmail(email, activationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("resendConfirm")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 401)]

        public async Task<IActionResult> ResendConfirmEmail([FromBody] ResendRequestAgain email)
        {
            var result = await _confirmAccountService.ResendEmailActivationLink(email);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize]
        [HttpPost]
        [Route("updatePassword")]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDTO password)
        {
            var result = await _updateAccount.UpdatePassword(password);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize]
        [HttpPost]
        [Route("updateName")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 409)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameRequestDTO name)
        {
            var result = await _updateAccount.UpdateName(name);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize]
        [HttpPost]
        [Route("updateEmail")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 409)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequestDTO email)
        {
            var result = await _updateAccount.UpdateEmail(email);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [Route("resend")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
        {
            var result = await _resetService.ForgotPassword(forgotPassword);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        [Route("confirmPasswordReset")]
        public async Task<IActionResult> ConfirmPasswordReset([FromQuery] string email, [FromQuery] string password, [FromQuery] string activationToken)
        {
            var result = await _resetService.ConfirmPasswordReset(email, password, activationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser([FromQuery] string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return Ok(user.Id);
            }
            return BadRequest();
        }
    }
}
