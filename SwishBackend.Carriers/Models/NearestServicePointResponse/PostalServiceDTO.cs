using System.ComponentModel.DataAnnotations.Schema;

namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class PostalServiceDTO
    {
        public int Id { get; set; }
        public string OpenDay { get; set; }
        public string CloseDay { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        [ForeignKey("OpeningHoursId")]
        public int OpeningHoursId { get; set; }
        public OpeningHoursDTO? OpeningHours { get; set; }
    }
}
