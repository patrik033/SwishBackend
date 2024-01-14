using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using SwishBackend.Payments.Models;
using SwishBackend.Payments.Models.Azure;

namespace SwishBackend.Payments.Stripe
{
    public class StripeService : IStripeService
    {
        private readonly AzureKeyVault _stripeKeyVaultKey;
        public StripeService(AzureKeyVault stripeKeyVaultKey)
        {
            _stripeKeyVaultKey = stripeKeyVaultKey;

        }

        public async Task<Session> CreateCheckOutSession(ShoppingCartOrder order)
        {
            StripeConfiguration.ApiKey = await _stripeKeyVaultKey.GetStripeSecretAsync();
            var domain = "http://localhost:5173";
            var lineItems = new List<SessionLineItemOptions>();


            order.ShoppingCartItems.ForEach(item => lineItems.Add(new SessionLineItemOptions
            {

                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = item.Price * 100,
                    Currency = "sek",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name,
                    }
                },
                Quantity = item.OrderedQuantity,
            }));

            var options = new SessionCreateOptions
            {
                //ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                //{
                //    AllowedCountries = new List<string>
                //    {
                //        "SE"
                //    },
                //},


                //PaymentMethodTypes = new List<string>
                //{
                //    "card",
                //},
                //BillingAddressCollection = "required",
                CustomerEmail = order.Email,
                UiMode = "embedded",
                LineItems = lineItems,
                Mode = "payment",
                ReturnUrl = domain + "/return?session_id={CHECKOUT_SESSION_ID}",
            };



            var service = new SessionService();
            Session session = service.Create(options);
            return session;

        }
    }
}
