using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
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

        public StripeController(OrdersDbContext context, IStripeService stripeService)
        {
            _context = context;
            _stripeService = stripeService;
        }


        [HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> CreateCheckoutSession(string userName)
        {

            var data = await _context.ShoppingCartOrders
                .Include(x => x.ShoppingCartItems)
                .FirstOrDefaultAsync(x => x.Email == userName && !x.HasBeenCheckedOut);

         
            var stripe = _stripeService.CreateCheckOutSession(data);

            
            return Ok(stripe);
        }

        [HttpGet]
        [Route("{session_id}")]
        public IActionResult SessionStatus(string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            return Ok(session);
        }
    }
}
