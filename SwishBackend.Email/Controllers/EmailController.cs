using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwishBackend.Email.Models.Azure;

namespace SwishBackend.Email.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly AzureKeyVault _keyService;


        public EmailController(AzureKeyVault keyService)
        {
            _keyService = keyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailKey()
        {
            var emaailKey = await _keyService.GetSendgridSecretAsync();
            return Ok(emaailKey);
        }
    }
}
