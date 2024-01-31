using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwishBackend.Carriers.Data;
using SwishBackend.Carriers.Middleware;
using SwishBackend.Carriers.Models;
using SwishBackend.Carriers.Models.AllServicePoints;
using SwishBackend.Carriers.Models.NearestServicePointDHLModels;
using SwishBackend.Carriers.Models.NearestServicePointPostNordModels;
using SwishBackend.Carriers.Models.NearestServicePointResponse;
using SwishBackend.Carriers.Profiles;
using System.Net;
using System.Text.Json;

namespace SwishBackend.Carriers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostNordController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly CarriersDbContext _context;
        private readonly AzureKeyVault _keyVault;

        public PostNordController(IMapper mapper,CarriersDbContext context,AzureKeyVault keyVault )
        {
            _client = new HttpClient();
            _mapper = mapper;
            _context = context;
            _keyVault = keyVault;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllServicePoints()
        {
            var host = "https://atapi2.postnord.com";
            var apiKey = await _keyVault.GetPostNordSecretAsync();
            var requestUri = $"{host}/rest/businesslocation/v5/servicepoints/information?apikey={apiKey}" +
                $"&returnType=json" +
                $"&countryCode=SE" +
                $"&agreementCountry=SE" +
                $"&context=optionalservicepoint" +
                $"&responseFilter=public" +
                $"&typeId=25";

            var response = await _client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var servicePointResponse = JsonSerializer.Deserialize<ServicePointData>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                

                var servicePoints = new List<Models.NearestServicePointPostNordModels.ServicePoint>();
                if (servicePointResponse != null && servicePointResponse != null)
                {
                    servicePoints = servicePointResponse.servicePointInformationResponse.servicePoints;
                    var simplifiedServicePoints = servicePoints;
                    var simplifiedServicePointsData = _mapper.Map<List<PostNordNearestServicePoint>>(servicePoints);

                   



                    await _context.PostNordNearestServicePoints.AddRangeAsync(simplifiedServicePointsData);
                    await _context.SaveChangesAsync();
                    int totalCount = simplifiedServicePointsData.Count;
                    return Ok(simplifiedServicePointsData);
                }

                return Ok(jsonString);
            }
            return BadRequest();


        }

        [HttpGet]
        [Route("/query")]
        public async Task<IActionResult> GetQuery()
        {
            //var data = _context.PostNordNearestServicePoints
            //    .Include(x => x.DeliveryAddress)
            //    .Include(x => x.Coordinates)
            //    .Include(x => x.OpeningHours.PostalServices).ToList()
            //    .FirstOrDefault(x => x.ServicePointId == "101562");



            var data = _context.PostNordNearestServicePoints
                .Include(x => x.DeliveryAddress).Include(x => x.Coordinates)
                .Include(x => x.OpeningHours.PostalServices).ToList()
                .Where(x => x.DeliveryAddress.City.ToLower() == "ESKILSTUNA".ToLower()).ToList();    
                

            return Ok(data);
        }



        [HttpPost]
        public async Task<IActionResult> GetNearestServicePoint([FromBody] AddressInputDTO address)
        {
            var host = "https://atapi2.postnord.com";
            var apiKey = await _keyVault.GetPostNordSecretAsync();
            var requestUri = 
                $"{host}/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey={apiKey}" +
                $"&returnType=json" +
                $"&countryCode=SE" +
                $"&agreementCountry=SE" +
                $"&city={address.City}" +
                $"&postalCode={address.ZipCode}" +
                $"&streetName={address.StreetAddress}" +
                $"&streetNumber={address.StreetNumber}" +
                $"&numberOfServicePoints=5" +
                $"&srId=EPSG:4326" +
                $"&context=optionalservicepoint&responseFilter=public" +
                $"&typeId=24,25,54" +
                $"&callback=jsonp";

            var response = await _client.GetAsync(requestUri);


            if (response.IsSuccessStatusCode)
            {
                // Read the JSON content
                var jsonString = await response.Content.ReadAsStringAsync();

                // Extract the JSON part from the JSONP data
                var jsonPart = ExtractJsonFromJsonp(jsonString);

                // Deserialize the JSON string into a dynamic object
                var servicePointResponse = JsonSerializer.Deserialize<ServicePointData>(jsonPart, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var servicePoints = new List<Models.NearestServicePointPostNordModels.ServicePoint>();
                if (servicePointResponse != null && servicePointResponse != null)
                {
                    servicePoints = servicePointResponse.servicePointInformationResponse.servicePoints;
                    var simplifiedServicePoints = servicePoints;
                    var simplifiedServicePointsData = _mapper.Map<List<PostNordNearestServicePoint>>(servicePoints);
                    return Ok(simplifiedServicePointsData);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }



        static string ExtractJsonFromJsonp(string jsonp)
        {
            // Extract the JSON part from JSONP data
            int startIndex = jsonp.IndexOf('(') + 1;
            int endIndex = jsonp.LastIndexOf(')');
            return jsonp.Substring(startIndex, endIndex - startIndex);
        }
    }
}
