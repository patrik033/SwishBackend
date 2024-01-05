using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Authentication
{
    public interface IUserRegistrationService
    {
        Task<ApiResponse> RegisterUserAsync(RegisterRequestDTO registerRequestDTO);
    }
}
