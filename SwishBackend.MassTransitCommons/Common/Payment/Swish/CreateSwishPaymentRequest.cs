using SwishBackend.MassTransitCommons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwishBackend.MassTransitCommons.Common.Payment.CreateSession
{
    public class CreateSwishPaymentRequest
    {
        public ShoppingCartOrderMessage PaymentOrder { get; set; }
        public string StreetAddress { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
