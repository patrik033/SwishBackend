namespace SwishBackend.Carriers.Models.NearestServicePointPostNordModels
{
    public class VisitingAddress
    {
        public string countryCode { get; set; }
        public string city { get; set; }
        public string streetName { get; set; }
        public string streetNumber { get; set; }
        public string postalCode { get; set; }
        public object additionalDescription { get; set; }
    }
}
