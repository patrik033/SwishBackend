using SendGrid;
using SendGrid.Helpers.Mail;
using SwishBackend.Email.Models.Azure;

namespace SwishBackend.Email.Email
{
    public class SendGridEmailRegisterService : ISendGridEmailRegisterService
    {
        private readonly AzureKeyVault _azureKeyVaultKey;

        public SendGridEmailRegisterService(AzureKeyVault azureKeyVaultKey)
        {
            _azureKeyVaultKey = azureKeyVaultKey;
        }

        public async Task<Response> SendAsync(string from, string to, string subject, string body, string name)
        {
            var emailApiKey = await _azureKeyVaultKey.GetSendgridSecretAsync();


            string mybody = $@"
                <html>
                    <head>
                        <style>
                            /* Your styles here */
                        </style>
                    </head>
                    <body>
                        <div style='font-family: Arial, sans-serif; background-color: #f5f5f5; text-align: center; padding: 20px;'>
                            <div style='max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
                                <h1 style='color: #3498db;'>Välkommen till vår community!</h1>
                                <p style='color: #333;'>Klicka på länken nedan för att bekräfta din registrering:</p>
                                <a href='{body}' style='display: inline-block; background-color: #3498db; color: #fff; padding: 10px 20px; text-decoration: none; border-radius: 5px; margin-top: 20px;'>Bekräfta registrering</a>
                            </div>
                        </div>
                    </body>
                </html>
                     ";


            var client = new SendGridClient(emailApiKey);
            var sendMessage = new SendGridMessage
            {
                From = new EmailAddress(from, "Test"),
                Subject = subject,
                HtmlContent = mybody,
                PlainTextContent = "Please confirm your registration by clicking the link below:\n" + body,

            };
            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(sendMessage.From, toEmail, sendMessage.Subject, sendMessage.PlainTextContent, mybody);
            var response = await client.SendEmailAsync(msg);


            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return response;
        }
    }
}
