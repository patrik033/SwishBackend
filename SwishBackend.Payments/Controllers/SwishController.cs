using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.Payments.Models;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace SwishBackend.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwishController : ControllerBase
    {


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
                       // ReadMessage(paymentResponse);
                    }
                    // Handle the deserialized paymentResponse object
                    // You can perform additional processing here if needed

                    return Ok(paymentResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return Ok(value);
        }
        [Route("qr")]
        [HttpPost]
        public async Task<IActionResult> CreateQr(string token)
        {
            var data = new
            {
                format = "png",
                size = 300,
                token = token
            };



            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(basePath, "Certificates", "Sandbox.p12");
            var cert = new X509Certificate2(path, "swish");
            AddCertificateToStore(cert, StoreLocation.CurrentUser, StoreName.Root);

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            var client = new HttpClient(handler);


            var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
            var response = await client
                .PostAsync($"https://staging.getswish.pub.tds.tieto.com/qrg-swish/api/v1/commerce"
                , new StringContent(jsonData, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsByteArrayAsync();

                return File(result, "image/png");

            }
            else
            {
                return BadRequest(response);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateSwishPaymentRequest request)
        {
            var data = new
            {
                
                payeePaymentReference = request.PaymentOrder.Id,
                callbackUrl = "https://gradually-peaceful-dove.ngrok-free.app/api/swish/callback",
                payeeAlias = "1230474494",
                currency = "SEK",
                amount = request.PaymentOrder.TotalPrice.ToString(),
                message = $"Order for {request.PaymentOrder.Id}"
            };



            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(basePath, "Certificates", "Sandbox.p12");
            var cert = new X509Certificate2(path, "swish");
            AddCertificateToStore(cert, StoreLocation.CurrentUser, StoreName.Root);

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            var client = new HttpClient(handler);

            var instructionsId = Guid.NewGuid();
            string uuid = instructionsId.ToString("N").ToUpper();
            var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
            var response = await client
                .PostAsync($"https://staging.getswish.pub.tds.tieto.com/swish-cpcapi/api/v1/paymentrequests"
                , new StringContent(jsonData, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("PaymentRequestToken", out var tokenValues))
                {
                    var paymentRequestToken = tokenValues.FirstOrDefault();
                    Console.WriteLine($"Payment request created. Token: {paymentRequestToken}");
                    // return paymentRequestToken;
                    return Ok(new { Id = uuid, token = paymentRequestToken, origin = response });
                }
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        private void AddCertificateToStore(X509Certificate2 certificate, StoreLocation location, StoreName storeName)
        {
            var store = new X509Store(storeName, location);

            // Öppna butiken för ändringar
            store.Open(OpenFlags.ReadWrite);

            try
            {
                // Kontrollera om certifikatet redan finns i butiken
                var existingCertificate = store.Certificates.Find(X509FindType.FindByThumbprint, certificate.Thumbprint, validOnly: false);
                if (existingCertificate.Count == 0)
                {
                    // Lägg bara till certifikatet om det inte redan finns i butiken
                    store.Add(certificate);
                }
                else
                {
                    Console.WriteLine("Certificate already exists in the store.");
                }
            }
            catch (Exception ex)
            {
                // Hantera fel här om något går fel
                Console.WriteLine($"Error adding certificate to store: {ex.Message}");
            }
            finally
            {
                // Stäng butiken efter ändringar
                store.Close();
            }
        }

      
    }
}
