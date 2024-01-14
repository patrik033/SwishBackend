using Stripe.Checkout;
using SwishBackend.Payments.Models;


namespace SwishBackend.Payments.Stripe
{
    public interface IStripeService
    {
        Task<Session> CreateCheckOutSession(ShoppingCartOrder order);
    }
}
