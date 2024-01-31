using System.ComponentModel.DataAnnotations.Schema;

namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class OpeningHoursDTO
    {
        public int Id { get; set; }
        public List<PostalServiceDTO>? PostalServices { get; set; }
        [ForeignKey("ServicePointId ")]
        public int ServicePointId { get; set; }
        public PostNordNearestServicePoint ServicePoint { get; set; }
    }
}
