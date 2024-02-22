using SendGrid;
using SendGrid.Helpers.Mail;
using Stripe;
using SwishBackend.Email.Models.Azure;
using System.Text;

namespace SwishBackend.Email.Email
{
    public class SendGridEmailPaymentSuccess : ISendGridEmailPaymentSuccess
    {
        private readonly AzureKeyVault _azureKeyVault;

        public SendGridEmailPaymentSuccess(AzureKeyVault azureKeyVault)
        {
            _azureKeyVault = azureKeyVault;
        }
        public async Task<Response> SendAsync(string from, string to, string subject, long? totalAmount, Address customeraddress, Address shippingAddress, List<LineItem> items)
        {
            var emailApiKey = await _azureKeyVault.GetSendgridSecretAsync();

            StringBuilder itemsHtml = new StringBuilder();
            if (items != null)
            {
                foreach (LineItem item in items)
                {
                    itemsHtml.Append($"<tr><td>{item.Description}</td><td>{((item.Price.UnitAmountDecimal.Value/100).ToString("C"))}</td></tr>");
                }
            }

            string receiptBody = $@"
                <html>
                    <head>
                        <style>
                            /* Your styles here */
                        </style>
                    </head>
                    <body>
                        <div style='font-family: Arial, sans-serif; background-color: #f5f5f5; text-align: center; padding: 20px;'>
                            <div style='max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
                                <h1 style='color: #3498db;'>Receipt for Your Order</h1>
                                <p style='color: #333;'>Thank you for your purchase! Below is the receipt for your order:</p>
                                <table style='width: 100%; border-collapse: collapse;'>
                                    <tr><th>Item</th><th>Price</th></tr>
                                    {itemsHtml}
                                </table>
                                <p style='color: #333;'>Total Amount: {((totalAmount.Value/100).ToString("C"))}</p>
                                <h2 style='color: #3498db;'>Customer Address</h2>
                                <p>City: {customeraddress.City}<br>Country: {customeraddress.Country}<br>Street: {customeraddress.Line1}<br>Postal Code: {customeraddress.PostalCode}</p>
                                <h2 style='color: #3498db;'>Shipping Address</h2>
                                <p>City: {shippingAddress.City}<br>Country: {shippingAddress.Country}<br>Street: {shippingAddress.Line1}<br>Postal Code: {shippingAddress.PostalCode}</p>
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
                HtmlContent = receiptBody,
                PlainTextContent = "",

            };
            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(sendMessage.From, toEmail, sendMessage.Subject, sendMessage.PlainTextContent, receiptBody);
            var response = await client.SendEmailAsync(msg);


            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return response;
        }
    }
}
