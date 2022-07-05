using System.Threading.Tasks;

namespace ZeroStack.IdentityServer.API.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
