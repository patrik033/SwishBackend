using Microsoft.AspNetCore.Mvc;
using SwishBackend.Identity.Utility;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Authentication
{
    public interface IUpdateAccount
    {
        Task<ApiResponse> UpdateName([FromBody] UpdateNameRequestDTO name);
        Task<ApiResponse> UpdateEmail([FromBody] UpdateEmailRequestDTO email);
        Task<ApiResponse> UpdatePassword([FromBody] UpdatePasswordRequestDTO password);
    }
}
