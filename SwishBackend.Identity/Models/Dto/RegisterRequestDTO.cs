using System.ComponentModel.DataAnnotations;

namespace SwishBackend.Models.Dto
{
    public class RegisterRequestDTO
    {
       
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
       
        public string? Role { get; set; }
    }
}
