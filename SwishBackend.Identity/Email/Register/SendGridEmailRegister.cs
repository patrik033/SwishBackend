using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SwishBackend.Identity.Email.Token;

namespace SwishBackend.Identity.Email.Register
{
    public class SendGridEmailRegister : ISendGridEmailRegister
    {

        private readonly IOptions<EmailSettings> _emailSettings;

        public SendGridEmailRegister(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task<Response> SendAsync(string from, string to, string subject, string token, string name)
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
                Token = token
            });

            var resp = await sendGridClient.SendEmailAsync(sendGridMessage);

            if (resp.IsSuccessStatusCode)
            {
                return resp;
            }
            return resp;
        }
    }
}
