using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Authentication
{
    public interface IUserLoginService
    {
        Task<ApiResponse> LoginUserAsync(LoginRequestDTO loginRequestDTO);
    }
}
