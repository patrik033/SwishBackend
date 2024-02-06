using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SwishBackend.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Address? Address { get; set; }
        public BillingAddress? BillingAddress { get; set; }
    }
}
