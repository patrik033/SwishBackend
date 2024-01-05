using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace SwishBackend.Identity.Email.Token
{
    public class SendGridEmailTokens : ISendGridEmailTokens
    {
        private readonly IOptions<EmailSettings> _emailSettings;

        public SendGridEmailTokens(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }
        public async Task<Response> SendAsync(string from, string to, string subject, string tokenLink, string name)
        {
            var environmentVariableKey = Environment.GetEnvironmentVariable("SendGrid");
            var resetTokenTemplate = Environment.GetEnvironmentVariable("ResetTokenTemplate");
            var sendGridClient = new SendGridClient(environmentVariableKey);
            var sendGridMessage = new SendGridMessage();

            sendGridMessage.SetFrom(from);
            sendGridMessage.AddTo(to);
            sendGridMessage.SetSubject(subject);
            sendGridMessage.SetTemplateId(resetTokenTemplate);
            sendGridMessage.SetTemplateData(new TokenTemplateData
            {
                Name = name,
                Token = tokenLink
            });

            var resp = await sendGridClient.SendEmailAsync(sendGridMessage);

            //var client = new SendGridClient(environmentVariableKey);

            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress(from, "WebApp Registration"),
            //    Subject = subject,
            //    //PlainTextContent = body,
            //    HtmlContent = $"<strong>{body}</strong>"
            //};

            //var response = await client.SendEmailAsync(msg);

            //msg.AddTo(new EmailAddress(to));
            if (resp.IsSuccessStatusCode)
            {
                return resp;
            }
            return resp;
        }
    }
}

