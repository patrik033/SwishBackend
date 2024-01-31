namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class DHLPlaceDTO
    {
        public DeliveryAddressDTO address { get; set; }
        public DHLGeoDTO geo { get; set; }
    }
}
