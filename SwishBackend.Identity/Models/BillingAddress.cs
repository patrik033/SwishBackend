using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwishBackend.Identity.Models
{
    public class BillingAddress
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string? StreetNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
