using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwishBackend.Carriers.Models.NearestServicePointDHLModels;
using SwishBackend.Carriers.Models.NearestServicePointResponse;
using SwishBackend.Carriers.Models;
using System.Text.Json;

namespace SwishBackend.Carriers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhlController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly AzureKeyVault _keyVault;

        public DhlController(IMapper mapper,AzureKeyVault keyVault)
        {
            _client = new HttpClient();
            _mapper = mapper;
            _keyVault = keyVault;
        }

        [HttpPost]
        public async Task<IActionResult> GetNearestServicePoint([FromBody] AddressInputDTO address)
        {

            var apiKey = await _keyVault.GetDhlSecretAsync();
            var requestUri =
                $"https://api-sandbox.dhl.com/location-finder/v1/find-by-address?countryCode=SE&addressLocality={address.City}" +
                $"&postalCode={address.ZipCode}" +
                $"&streetAddress={address.StreetAddress}" +
                $"&providerType=parcel" +
                $"&locationType=servicepoint" +
                $"&radius=2500" +
                $"&limit=5" +
                $"&hideClosedLocations=true";

            _client.BaseAddress = new Uri(requestUri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("DHL-API-Key", apiKey);
            var response = await _client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var dhlResponse = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<DHLServicePointResponse>(dhlResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var mappedResponse = _mapper.Map<List<DHLNearestServicePoint>>(deserialized.locations);
                return Ok(mappedResponse);
            }
            return BadRequest(response.StatusCode);
        }
    }
}
