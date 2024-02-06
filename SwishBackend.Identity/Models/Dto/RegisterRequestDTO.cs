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



        [Required(ErrorMessage = "Street address is required")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Street number is required")]

        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }



        public string? BillingStreetAddress { get; set; }

        public string? BillingStreetNumber { get; set; }

        public string? BillingCity { get; set; }

        public string? BillingZipCode { get; set; }

    }
}
