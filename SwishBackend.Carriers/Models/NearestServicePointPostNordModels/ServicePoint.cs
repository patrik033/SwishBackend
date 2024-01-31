namespace SwishBackend.Carriers.Models.NearestServicePointPostNordModels
{
    public class ServicePoint
    {
        public string Name { get; set; }
        public string ServicePointId { get; set; }
        public string PudoId { get; set; }
        public object RoutingCode { get; set; }
        public object HandlingOffice { get; set; }
        public object LocationDetail { get; set; }
        public int? RouteDistance { get; set; }
        public Pickup Pickup { get; set; }
        public VisitingAddress VisitingAddress { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public NotificationArea NotificationArea { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public Type Type { get; set; }
    }
}
