namespace SwishBackend.Identity.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
