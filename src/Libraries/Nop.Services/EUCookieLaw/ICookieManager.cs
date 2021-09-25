using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;

namespace Nop.Services.EUCookieLaw
{
    public interface ICookieManager
    {
        bool IsProviderAllowed<T>() where T : ICookieProvider, new();
        Task UpdateCookieAcceptance(string allowedPurposes);
    }
}