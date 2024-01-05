using MassTransit;
using MassTransitCommons.Common.Email;
using SwishBackend.Email.Email;
using SwishBackend.Email.Models.Azure;

namespace SwishBackend.Email.Models.MessageBus
{
    public class EmailRegistrationSuccessfullConsumer : IConsumer<EmailConfirmationMessage>
    {
        private readonly ISendGridEmailRegisterService _sendGridEmailRegisterService;
        private readonly AzureKeyVault _azureKeyVaultKey;
        public EmailRegistrationSuccessfullConsumer(ISendGridEmailRegisterService sendGridEmailRegisterService, AzureKeyVault azureKeyVaultKey)
        {
            _sendGridEmailRegisterService = sendGridEmailRegisterService;
            _azureKeyVaultKey = azureKeyVaultKey;
        }

        public async Task Consume(ConsumeContext<EmailConfirmationMessage> context)
        {
            var data = context.Message;
            var sendGridKey = await _azureKeyVaultKey.GetSendgridSecretAsync();
            var from = await _azureKeyVaultKey.GetSenderEmailSecretAsync();

            if (data != null && sendGridKey != null && from != null)
            {
                var result = await _sendGridEmailRegisterService.SendAsync(from, data.Email, "Confirm your email", data.EmailToken, data.UserName);
            }
        }
    }
}
