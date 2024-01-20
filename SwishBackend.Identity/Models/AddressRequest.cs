namespace SwishBackend.Identity.Models
{
    public class AddressRequest
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
    }
}
