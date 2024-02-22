using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwishBackend.MassTransitCommons.Common.Email
{
    public class EmailPaymentSuccessMessage
    {
        public string? CustomerEmail { get; set; }
        public long? AmountTotal { get; set; }
        public Address CustomerAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}
