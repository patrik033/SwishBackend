using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwishBackend.Payments.Controllers
{
    [Route("api/callback")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        [HttpPost]
        public IActionResult HandleCallback([FromBody] dynamic data)
        {
            // Process the callback data
            // You can access the data received from the callback request here

            return Ok("Callback received successfully");
        }
    }
}
