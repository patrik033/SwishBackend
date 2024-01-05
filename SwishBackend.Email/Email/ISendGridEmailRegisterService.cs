using SendGrid;

namespace SwishBackend.Email.Email
{
    public interface ISendGridEmailRegisterService
    {
        Task<Response> SendAsync(string from, string to, string subject, string body, string name);
    }
}
