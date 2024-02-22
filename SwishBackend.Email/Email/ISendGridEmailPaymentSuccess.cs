using SendGrid;
using Stripe;

namespace SwishBackend.Email.Email
{
    public interface ISendGridEmailPaymentSuccess
    {
        Task<Response> SendAsync(string from, string to, string subject,long ?totalAmount,Address customerAddress,Address shippingAddress,List<LineItem> orderItems);
    }
}
