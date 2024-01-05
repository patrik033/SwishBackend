using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwishBackend.MassTransitCommons.Common
{
    public class ProductUpdated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }
        public ProductType Type { get; set; }

        public Guid ProductCategoryId { get; set; }
    }
}
