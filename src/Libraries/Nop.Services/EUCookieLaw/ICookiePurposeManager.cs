using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;

namespace Nop.Services.EUCookieLaw
{
    public interface ICookiePurposeManager
    {
        Task<bool> IsProviderAllowed(ICookieProvider provider);
        Task<bool> IsProviderAllowed<T>() where T : ICookieProvider, new();
        Task<bool> IsPurposeAllowed(ICookiePurpose purpose);
        Task<bool> IsPurposeAllowed<T>() where T : ICookiePurpose, new();
        Task SetAllowed(string allowedPurposes);
    }
}