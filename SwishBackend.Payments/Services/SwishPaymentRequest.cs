using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SwishBackend.Payments.Services
{
    public class SwishPaymentRequest
    {

        //TODO: läs in filen från rätt plats, skicka in objektet som en klass eller record
        //TODO: returnera svaret, konfigurera vad som returneras och vad som ska hända då ==> kolla swish dokumentationen för detta.
        //TODO: kolla upp om returnurl kan vara på localhost?
        public  async Task CreatePaymentRequest()
        {

            var data = new
            {
                payeePaymentReference = "0123456789",
                callbackUrl = "https://example.com/swishcallback",
                payeeAlias = "1234679304",
                currency = "SEK",
                payerAlias = "4671234768",
                amount = "100",
                message = "Kingston USB Flash Drive 8 GB"
            };



            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(basePath, "Certificates", "Swish_Merchant_TestCertificate_1234679304.p12");
            var cert = new X509Certificate2("Swish_Merchant_TestCertificate_1234679304.p12","swish",X509KeyStorageFlags.Exportable);
           
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            var client = new HttpClient(handler);

            var instructionsId = Guid.NewGuid().ToString();
            var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
            var response = await client
                .PutAsync($"https://mss.cpc.getswish.net/swish-cpcapi/api/v2/paymentrequests/{instructionsId}"
                , new StringContent(jsonData, Encoding.UTF8, "application/json"));

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment request created");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
