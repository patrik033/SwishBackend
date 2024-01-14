using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.MassTransitCommons.Common.Payment.GetSession;
using SwishBackend.MassTransitCommons.Models;
using SwishBackend.Orders.Data;
using SwishBackend.Orders.Models;
using SwishBackend.Orders.Stripe;

namespace SwishBackend.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly OrdersDbContext _context;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;
        private readonly IRequestClient<CreatePaymentSessionRequest> _requestClient;
        private readonly IRequestClient<SessionStatusRequest> _getStripeSessionClient;

        public StripeController(
            OrdersDbContext context,
            IStripeService stripeService,
            IMapper mapper,
            IRequestClient<CreatePaymentSessionRequest> requestClient,
            IRequestClient<SessionStatusRequest> getStripeSessionClient
            )
        {
            _context = context;
            _stripeService = stripeService;
            _mapper = mapper;
            _requestClient = requestClient;
            _getStripeSessionClient = getStripeSessionClient;
        }


        [HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> CreateCheckoutSession(string userName)
        {

            var data = await _context.ShoppingCartOrders
                .Include(x => x.ShoppingCartItems)
                .FirstOrDefaultAsync(x => x.Email == userName && !x.HasBeenCheckedOut);

            if (data != null)
            {
                try
                {
                    var paymentOrder = _mapper.Map<ShoppingCartOrderMessage>(data);
                    var stripe = await _requestClient.GetResponse<CreatePaymentSessionResponse>(new CreatePaymentSessionRequest
                    {
                        PaymentOrder = paymentOrder
                    });

                    return Ok(stripe.Message.Session);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("No order exists for this user");


        }

        [HttpGet]
        [Route("{session_id}")]
        public async Task<IActionResult> SessionStatus(string session_id)
        {
            try
            {
                var sessiongetReq = new SessionStatusRequest { SessionId = session_id };
                var getStripe = await _getStripeSessionClient.GetResponse<SessionStatusResponse>(new SessionStatusRequest
                {
                    SessionId = session_id
                });

                if (getStripe.Message.SessionResponse != null)
                {
                    //TODO skicka mailbekräftelse vid lyckad betalning
                    var data = await _context.ShoppingCartOrders
                        .Include(x => x.ShoppingCartItems)
                        .FirstOrDefaultAsync(x => x.Email == getStripe.Message.SessionResponse.CustomerEmail && !x.HasBeenCheckedOut);

                    data.HasBeenCheckedOut = true;
                    _context.SaveChanges();
                    return Ok(getStripe.Message.SessionResponse);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return BadRequest($"Session status: {ex.Message}");
            }
        }
    }
}
