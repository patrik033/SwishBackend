using AutoMapper;
using MassTransit;
using Stripe;
using Stripe.Checkout;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.Payments.Models.Azure;
using SwishBackend.Payments.Services;
using SwishBackend.Payments.Stripe;

namespace SwishBackend.Payments.Consumer
{
    public class StripeSessionCreateConsumer : IConsumer<CreateStripePaymentSessionRequest>
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


        public async Task Consume(ConsumeContext<CreateStripePaymentSessionRequest> context)
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
                       // UnitAmount = (long)item.Price,
                        Currency = "sek",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                        }
                    },
                    Quantity = item.OrderedQuantity,
                }));


                var customer = new CustomerCreateOptions
                {
                   Email = data.Email,
        
                    Shipping = new ShippingOptions
                   {
                       Name = "kklk",
                      
                      
                       Address = new AddressOptions
                       {
                           City = context.Message.City,
                           Country = "SE",
                           Line1 = $"{context.Message.StreetAddress} {context.Message.StreetNumber}",
                           PostalCode = context.Message.ZipCode
                           
                       }
                       
                   }
                };

                



                var customerService = new CustomerService();
                Customer sessions =  customerService.Create(customer);

                var options = new SessionCreateOptions
                {
                    
                   Customer = sessions.Id,
                    //CustomerEmail = data.Email,
                    UiMode = "embedded",
                    LineItems = lineItems,
                    Mode = "payment",

                    PhoneNumberCollection = new SessionPhoneNumberCollectionOptions
                    {
                        Enabled = true,
                    },

                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                        "swish"
                    },

                    ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                   {
                       AllowedCountries = new List<string>
                       {
                           "SE"
                       }
                   },
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

                await context.RespondAsync<CreateStripePaymentSessionResponse>(new CreateStripePaymentSessionResponse
                {
                    Session = new Session
                    {

                        Id = session.Id,
                        PhoneNumberCollection = session.PhoneNumberCollection,
                        BillingAddressCollection = session.BillingAddressCollection,
                        ShippingAddressCollection = session.ShippingAddressCollection,
                        Customer = session.Customer,
                        Status = session.Status,
                        ClientSecret = session.ClientSecret,
                        CustomerEmail = session.CustomerEmail,
                        
                        AmountTotal = session.AmountTotal,
                        InvoiceId = session.InvoiceId,
                        InvoiceCreation = session.InvoiceCreation,
                        Invoice = session.Invoice,
                        CustomerDetails = session.CustomerDetails,
                        ShippingDetails = session.ShippingDetails,
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
