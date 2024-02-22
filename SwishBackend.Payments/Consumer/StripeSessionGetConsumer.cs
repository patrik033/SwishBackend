using AutoMapper;
using MassTransit;
using Stripe;
using Stripe.Checkout;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.MassTransitCommons.Common.Payment.GetSession;
using SwishBackend.Payments.Models.Azure;
using SwishBackend.Payments.Stripe;

namespace SwishBackend.Payments.Consumer
{
    public class StripeSessionGetConsumer : IConsumer<SessionStatusRequest>
    {
        private readonly IStripeService _stripeService;
        private readonly AzureKeyVault _stripeKeyVaultKey;

        public StripeSessionGetConsumer(IStripeService stripeService, AzureKeyVault stripeKeyVaultKey)
        {
            _stripeService = stripeService;
            _stripeKeyVaultKey = stripeKeyVaultKey;
        }

        public async Task Consume(ConsumeContext<SessionStatusRequest> context)
        {
            try
            {

                
                var sessionService = new SessionService();
                Session session = sessionService.Get(context.Message.SessionId);

                await context.RespondAsync<SessionStatusResponse>(new SessionStatusResponse
                {
                    SessionResponse = new Session
                    {
                        Id = session.Id,
                        Status = session.Status,
                        InvoiceId = session.InvoiceId,
                        ClientSecret = session.ClientSecret,
                        CustomerEmail = session.CustomerEmail,
                        AmountTotal = session.AmountTotal,
                        CustomerDetails = session.CustomerDetails,
                        ShippingDetails = session.ShippingDetails,
                        BillingAddressCollection = session.BillingAddressCollection,
                        ShippingOptions = session.ShippingOptions,
                        TotalDetails = session.TotalDetails,
                        LineItems = session.LineItems,
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
