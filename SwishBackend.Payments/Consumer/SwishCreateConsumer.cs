using MassTransit;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.MassTransitCommons.Common.Payment.Swish;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SwishBackend.Payments.Consumer
{
    public class SwishCreateConsumer : IConsumer<CreateSwishPaymentRequest>
    {
        public SwishCreateConsumer()
        {

        }

        public async Task Consume(ConsumeContext<CreateSwishPaymentRequest> context)
        {

            try
            {
                var data = new
                {
                    payeePaymentReference = context.Message.PaymentOrder.Id.ToString("N").ToUpper(),
                    callbackUrl = "https://gradually-peaceful-dove.ngrok-free.app/api/swish/callback",
                    payeeAlias = "1230474494",
                    currency = "SEK",
                    amount = context.Message.PaymentOrder.TotalPrice.ToString(),
                    message = $"Order for {context.Message.PaymentOrder.Id}"
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
                        var result = await CreateQr(paymentRequestToken);
                        if (result != null)
                        {
                            await context.RespondAsync<CreateSwishPaymentResponse>(new CreateSwishPaymentResponse
                            {
                                QrData = result
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        private async Task<byte[]> CreateQr(string token)
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
                return result;

            }
            return null;
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
