namespace SwishBackend.Carriers.Models.NearestServicePointDHLModels
{
    // DTOs for DHL ServicePoints
    public class DHLServicePointDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DHLAddressDTO DeliveryAddress { get; set; }
        public List<DHLOpeningHoursDTO> OpeningHours { get; set; }
        public  DHLGeoDTOFirst Geo { get; set; }

        // Add other properties as needed
    }
}
