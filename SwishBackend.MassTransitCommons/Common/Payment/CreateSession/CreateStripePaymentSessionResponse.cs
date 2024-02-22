using Stripe.Checkout;

namespace SwishBackend.MassTransitCommons.Common.Payment.CreateSession
{
    public class CreateStripePaymentSessionResponse
    {
        public Session Session { get; set; }
    }
}
