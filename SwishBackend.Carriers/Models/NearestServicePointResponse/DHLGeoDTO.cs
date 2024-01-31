using System.ComponentModel.DataAnnotations.Schema;

namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class DHLGeoDTO
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [ForeignKey("ServicePointId  ")]
        public int ServicePointId { get; set; }
        public  PostNordNearestServicePoint ServicePoint { get; set; }
    }
}
