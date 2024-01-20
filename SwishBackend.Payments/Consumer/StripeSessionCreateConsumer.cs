using AutoMapper;
using MassTransit;
using Stripe;
using Stripe.Checkout;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.Payments.Models.Azure;
using SwishBackend.Payments.Stripe;

namespace SwishBackend.Payments.Consumer
{
    public class StripeSessionCreateConsumer : IConsumer<CreatePaymentSessionRequest>
    {

        private readonly IMapper _mapper;
        private readonly IStripeService _stripeService;
        private readonly AzureKeyVault _stripeKeyVaultKey;

        public StripeSessionCreateConsumer(IStripeService stripeService, AzureKeyVault stripeKeyVaultKey, IMapper mapper)
        {

            _stripeService = stripeService;
            _stripeKeyVaultKey = stripeKeyVaultKey;
            _mapper = mapper;
        }


        public async Task Consume(ConsumeContext<CreatePaymentSessionRequest> context)
        {
            try
            {
                var data = context.Message.PaymentOrder;


                StripeConfiguration.ApiKey = await _stripeKeyVaultKey.GetStripeSecretAsync();
                var domain = "http://localhost:5173";
                var lineItems = new List<SessionLineItemOptions>();

               
                data.ShoppingCartItems.ForEach(item => lineItems.Add(new SessionLineItemOptions
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

                    BillingAddressCollection = "required",
                    CustomerEmail = data.Email,
                    UiMode = "embedded",
                    LineItems = lineItems,
               
                    Mode = "payment",
                    InvoiceCreation = new SessionInvoiceCreationOptions
                    {
                        
                        Enabled = true,
                        InvoiceData = new SessionInvoiceCreationInvoiceDataOptions
                        {
                            
                            Description = $"Invoide for Product"
                        }
                    },
                    ReturnUrl = domain + "/return?session_id={CHECKOUT_SESSION_ID}",
                };



                var service = new SessionService();
                Session session = service.Create(options);
                //var mappedSession = _mapper.Map<CreatePaymentSessionResponse>(session);
                //return session;

                await context.RespondAsync<CreatePaymentSessionResponse>(new CreatePaymentSessionResponse
                {
                    Session = new Session
                    {
                        Id = session.Id,
                        BillingAddressCollection = session.BillingAddressCollection,
                        ShippingAddressCollection = session.ShippingAddressCollection,
                        InvoiceId = session.InvoiceId,
                        ClientSecret = session.ClientSecret,
                        CustomerEmail = session.CustomerEmail,
                        AmountTotal = session.AmountTotal,
                        InvoiceCreation = session.InvoiceCreation,
                        Invoice = session.Invoice,
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
