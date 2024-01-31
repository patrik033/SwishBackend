namespace SwishBackend.Carriers.Models.NearestServicePointDHLModels
{
    public class DHLServicePoint
    {
        public List<DHLServicePointId> ids { get; set; }
        public string keyword { get; set; }
        public string keywordId { get; set; }
        public string type { get; set; }
        // Add other properties as needed
    }
}
