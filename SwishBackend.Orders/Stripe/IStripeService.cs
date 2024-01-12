using Stripe.Checkout;
using SwishBackend.Orders.Models;

namespace SwishBackend.Orders.Stripe
{
    public interface IStripeService
    {
        Task<Session> CreateCheckOutSession(ShoppingCartOrder order);
    }
}
