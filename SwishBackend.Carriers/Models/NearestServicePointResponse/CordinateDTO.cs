using System.ComponentModel.DataAnnotations.Schema;

namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class CordinateDTO
    {
        public int Id { get; set; }
        public string countryCode { get; set; }
        public double northing { get; set; }
        public double easting { get; set; }
        public string srId { get; set; }

        [ForeignKey("ServicePointId ")]
        public int ServicePointId { get; set; }
        public  PostNordNearestServicePoint? ServicePoint { get; set; }

    }
}
