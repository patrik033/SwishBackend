using System.ComponentModel.DataAnnotations.Schema;

namespace SwishBackend.Carriers.Models.NearestServicePointResponse
{
    public class DeliveryAddressDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }

        [ForeignKey("ServicePointId ")]
        public int ServicePointId { get; set; }
        public PostNordNearestServicePoint ServicePoint { get; set; }

    }
}
