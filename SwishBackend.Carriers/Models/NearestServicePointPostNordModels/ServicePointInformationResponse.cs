namespace SwishBackend.Carriers.Models.NearestServicePointPostNordModels
{
    public class ServicePointInformationResponse
    {
        public List<CustomerSupport> customerSupports { get; set; }
        public List<ServicePoint> servicePoints { get; set; }
    }
}
