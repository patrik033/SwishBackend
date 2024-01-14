using Microsoft.EntityFrameworkCore;

namespace SwishBackend.Payments.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }
        public int OrderedQuantity { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ShoppingCartOrder? ShoppingCartOrder { get; set; }
    }
}
