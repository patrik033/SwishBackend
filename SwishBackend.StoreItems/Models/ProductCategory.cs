namespace SwishBackend.StoreItems.Models
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }

        // Navigation property for products in this category
        public List<Product> Products { get; set; }
    }
}
