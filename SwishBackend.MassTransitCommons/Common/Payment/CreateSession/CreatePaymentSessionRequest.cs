using SwishBackend.MassTransitCommons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwishBackend.MassTransitCommons.Common.Payment.CreateSession
{
    public class CreatePaymentSessionRequest
    {
        public ShoppingCartOrderMessage PaymentOrder { get; set; }
    }
}
