using Microsoft.AspNetCore.Identity;


namespace SwishBackend.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }
    }
}
