namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class DHLNearestServicePoint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DeliveryAddressDTO DeliveryAddress { get; set; }
        public OpeningHoursDTO OpeningHours { get; set; }
        public List<CordinateDTO>? Coordinates { get; set; }
        public DHLGeoDTO? Geo { get; set; }

    }
}
