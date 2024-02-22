using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwishBackend.MassTransitCommons.Common.Payment.Swish
{
    public class CreateSwishPaymentResponse
    {
        public byte[] QrData { get; set; }
    }
}
