using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.IO;
using SwishBackend.Orders.Models.Azure;
using Stripe.Checkout;
using SwishBackend.Orders.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using MassTransitCommons.Common.Email;
using SwishBackend.MassTransitCommons.Common.Email;

namespace SwishBackend.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebHook : ControllerBase
    {

        private readonly AzureKeyVault _stripeKey;
        private readonly OrdersDbContext _context;
        private readonly IPublishEndpoint _endpoint;

        public StripeWebHook(AzureKeyVault stripeKey, OrdersDbContext context, IPublishEndpoint endpoint)
        {
            _stripeKey = stripeKey;
            _context = context;
            _endpoint = endpoint;
        }
        [HttpPost]
        public async Task<IActionResult> WebHook()
        {
            const string secret = "whsec_01c1b2000426bc8cafc7d84fd2a331234bdcfa60991c34df668c5e33c6e05fb9";
            StripeConfiguration.ApiKey = await _stripeKey.GetStripeSecretAsync();
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility
                    .ConstructEvent
                    (json,
                    Request.Headers["Stripe-Signature"],
                    secret, 300, false);


                var stripeSesson = new Session();

                switch (stripeEvent.Type)
                {
                    case Events.CheckoutSessionCompleted:
                        stripeSesson = stripeEvent.Data.Object as Session;

                        var orderPaid = stripeSesson.PaymentStatus == "paid";
                        if (orderPaid)
                        {
                            var service = new SessionService();
                            var line = service.ListLineItems(stripeSesson.Id);
                            EmailCustomerAboutSuccessfullPayment(stripeSesson, line);
                        }
                        break;
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e);
            }
        }


        private void CreateOrder(Session session)
        {
            Console.WriteLine(session);
            // TODO: fill me in
        }

        private async Task EmailCustomerAboutSuccessfullPayment(Session session, StripeList<LineItem> lineItems)
        {

            try
            {
                var amountTotal = session?.AmountTotal;
                var email = session.CustomerEmail;
                var customerData = session.CustomerDetails.Address;
                var shippingAddress = session.Customer.Shipping.Address;


                var emailObject = new EmailPaymentSuccessMessage
                {
                    CustomerEmail = email,
                    AmountTotal = amountTotal,
                    ShippingAddress = shippingAddress,
                    CustomerAddress = customerData,
                    LineItems = lineItems.ToList(),
                };


                await _endpoint.Publish(emailObject);
                Console.WriteLine(session);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            // TODO: fill me in
        }
    }
}
