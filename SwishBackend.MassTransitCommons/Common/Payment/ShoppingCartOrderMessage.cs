

namespace SwishBackend.MassTransitCommons.Models
{
    public class ShoppingCartOrderMessage
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItemMessage> ShoppingCartItems { get; set; } 
        public bool HasBeenCheckedOut { get; set; } = false;
        public DateTime OrderTime { get; set; }

        public decimal? TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public string Email { get; set; }
    }
}
