using System.Threading.Tasks;

namespace ZeroStack.IdentityServer.API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
