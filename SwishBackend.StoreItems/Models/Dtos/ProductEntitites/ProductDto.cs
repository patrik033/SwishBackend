namespace SwishBackend.StoreItems.Models.Dtos.ProductEntitites
{
    public class ProductDto
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }

        public Guid ProductCategoryId { get; set; }
    }
}
