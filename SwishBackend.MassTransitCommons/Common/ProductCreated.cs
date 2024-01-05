
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SwishBackend.MassTransitCommons.Common
{
    public class ProductCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType Type { get; set; }

        public Guid ProductCategoryId { get; set; }

    }
}
