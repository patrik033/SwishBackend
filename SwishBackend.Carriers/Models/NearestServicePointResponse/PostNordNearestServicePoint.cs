using System.ComponentModel.DataAnnotations;

namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class PostNordNearestServicePoint
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServicePointId { get; set; }

        public DeliveryAddressDTO? DeliveryAddress { get; set; }


        public OpeningHoursDTO? OpeningHours { get; set; }

        public List<CordinateDTO>? Coordinates { get; set; }

        public DHLGeoDTO? Geo { get; set; }

    }
}
