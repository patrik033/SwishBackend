namespace SwishBackend.Orders.Models
{
    public class PostOrderRequestObject
    {
        public string userName { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
    }
}
