using Stripe.Checkout;

namespace SwishBackend.MassTransitCommons.Common.Payment.CreateSession
{
    public class CreatePaymentSessionResponse
    {
        public Session Session { get; set; }
    }
}
