using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MassTransitCommons.Common.Order
{
    public class ProductResponseMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }

        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }

       // [JsonConverter(typeof(JsonStringEnumConverter))]

        //public ProductType? Type { get; set; }
        // Foreign key for ProductCategory
        public Guid CategoryId { get; set; }
    }
}
