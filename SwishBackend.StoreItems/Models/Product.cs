using System.Text.Json.Serialization;

namespace SwishBackend.StoreItems.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]

        public ProductType? Type { get; set; }
        // Foreign key for ProductCategory
        public Guid ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
    }
}
