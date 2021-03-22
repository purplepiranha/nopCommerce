using System.Threading.Tasks;

namespace Nop.Extensions.MailerSend.Clients
{
    public interface IMailerSendClient
    {
        bool NoToken { get; }

        Task SendCustomerWelcomeEmailAsync(int customerId, string emailAddress, string name);
    }
}