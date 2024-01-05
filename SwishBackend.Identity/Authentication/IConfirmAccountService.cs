using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Authentication
{
    public interface IConfirmAccountService
    {
        Task<ApiResponse> ConfirmEmail([FromQuery] string email, [FromQuery] string activationToken);
        Task<ApiResponse> ResendEmailActivationLink([FromBody] ResendRequestAgain email);
    }
}
