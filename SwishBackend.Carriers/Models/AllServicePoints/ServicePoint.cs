namespace SwishBackend.Carriers.Models.AllServicePoints
{
    public class ServicePoint
    {
        public string name { get; set; }
        public string servicePointId { get; set; }
        public string pudoId { get; set; }
        public object routingCode { get; set; }
        public object handlingOffice { get; set; }
        public object locationDetail { get; set; }
        public object routeDistance { get; set; }
        public Pickup pickup { get; set; }
        public VisitingAddress visitingAddress { get; set; }
        public DeliveryAddress deliveryAddress { get; set; }
        public NotificationArea notificationArea { get; set; }
        public List<Coordinate> coordinates { get; set; }
        public OpeningHours openingHours { get; set; }
        public Type type { get; set; }
    }



}
