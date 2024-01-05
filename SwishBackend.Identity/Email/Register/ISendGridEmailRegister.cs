
using SendGrid;

namespace SwishBackend.Identity.Email.Register
{
    public interface ISendGridEmailRegister
    {
        Task<Response> SendAsync(string from, string to, string subject, string body, string name);
    }
}
