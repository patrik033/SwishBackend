

namespace SwishBackend.MassTransitCommons.Models
{
    public class ShoppingCartItemMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }

        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }
        public int OrderedQuantity { get; set; }
        public Guid ProductCategoryId { get; set; }
        //public ShoppingCartOrderMessage? ShoppingCartOrder { get; set; }
    }
}
