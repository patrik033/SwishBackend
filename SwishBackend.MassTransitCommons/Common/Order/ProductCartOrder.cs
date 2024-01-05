using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitCommons.Common.Order
{
    public class ProductCartOrder
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public ProductResponseMessage Product { get; set; }
    }
}
