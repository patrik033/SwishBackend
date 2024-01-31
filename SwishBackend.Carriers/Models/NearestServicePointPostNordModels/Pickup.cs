namespace SwishBackend.Carriers.Models.NearestServicePointPostNordModels
{
    public class Pickup
    {
        public object cashOnDelivery { get; set; }
        public List<object> heavyGoodsProducts { get; set; }
        public List<Product> products { get; set; }
    }
}
