using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.MassTransitCommons.Common.Payment.Swish;
using SwishBackend.MassTransitCommons.Models;
using SwishBackend.Orders.Data;
using SwishBackend.Orders.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using static MassTransit.ValidationResultExtensions;

namespace SwishBackend.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwishController : ControllerBase
    {

        private readonly OrdersDbContext _context;
        private readonly IMapper _mapper;
        IRequestClient<CreateSwishPaymentRequest> _requestClient;
        public SwishController(OrdersDbContext context,IMapper mapper,IRequestClient<CreateSwishPaymentRequest> requestClient)
        {
            _context = context;
            _mapper = mapper;
            _requestClient = requestClient;
        }


        [HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> CreateRequest(string userName, [FromBody] AddressRequest addressDetails)
        {

            var currentOrder = await _context.ShoppingCartOrders
                .Include(x => x.ShoppingCartItems)
                .FirstOrDefaultAsync(x => x.Email == userName && !x.HasBeenCheckedOut);

            if (currentOrder != null || addressDetails != null)
            {

                var paymentOrder = _mapper.Map<ShoppingCartOrderMessage>(currentOrder);
                var swish = await _requestClient.GetResponse<CreateSwishPaymentResponse>(new CreateSwishPaymentRequest
                {
                    PaymentOrder = paymentOrder,
                    City = addressDetails.City,
                    StreetAddress = addressDetails.StreetAddress,
                    StreetNumber = addressDetails.StreetNumber,
                    ZipCode = addressDetails.ZipCode,
                });

                return File(swish.Message.QrData, "image/png");
            }
            return BadRequest();
        }

       

        [HttpPost]
        [Route("callback")]
        public async Task<IActionResult> PostRequestValue(object value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(value.ToString());
                    ms.Write(byteArray, 0, byteArray.Length);
                    ms.Seek(0, SeekOrigin.Begin);

                    // Deserialize the stream into PaymentResponse object
                    var paymentResponse = await JsonSerializer.DeserializeAsync<CallbackResponse>(ms);

                    if (paymentResponse != null)
                    {
                        ReadMessage(paymentResponse);
                    }
                 

                    return Ok(paymentResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok(value);
        }

      


        //handle database stuff => for status paid,canceled & error
        //if error & status dont delete order(might change) => create a new object in finalized orders

        //get all items based on the paymentreference(the orderId)
        //create a paid object
        //when it's created send email => send to signalR => update the order item to checked out

        //if not paid => send to signalR => update with payment error and maybe a reference
        private void ReadMessage(CallbackResponse response)
        {

            //response.


            
        }
    }
}
