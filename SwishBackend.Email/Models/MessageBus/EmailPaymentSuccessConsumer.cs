using MassTransit;
using SwishBackend.Email.Email;
using SwishBackend.Email.Models.Azure;
using SwishBackend.MassTransitCommons.Common.Email;

namespace SwishBackend.Email.Models.MessageBus
{


    public class EmailPaymentSuccessConsumer : IConsumer<EmailPaymentSuccessMessage>
    {
        private readonly ISendGridEmailPaymentSuccess _sendGridEmailPaymentSuccess;
        private readonly AzureKeyVault _azureKeyVaultKey;
        public EmailPaymentSuccessConsumer(ISendGridEmailPaymentSuccess sendGridEmailPaymentSuccess, AzureKeyVault azureKeyVaultKey)
        {
            _sendGridEmailPaymentSuccess = sendGridEmailPaymentSuccess;
            _azureKeyVaultKey = azureKeyVaultKey;
        }

        public async Task Consume(ConsumeContext<EmailPaymentSuccessMessage> context)
        {
            var data = context.Message;
            var sendGridKey = await _azureKeyVaultKey.GetSendgridSecretAsync();
            var from = await _azureKeyVaultKey.GetSenderEmailSecretAsync();

            if (data != null && sendGridKey != null && from != null)
            {
                var result = await _sendGridEmailPaymentSuccess.SendAsync(from, data.CustomerEmail, "Order receipt", data.AmountTotal, data.CustomerAddress,data.ShippingAddress,data.LineItems);
            }
        }
    }
}
