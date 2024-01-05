using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Authentication
{
    public interface IResetService
    {
        Task<ApiResponse> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword);
        Task<ApiResponse> ConfirmPasswordReset([FromQuery] string email, [FromQuery] string password, [FromQuery] string activationToken);
    }
}
