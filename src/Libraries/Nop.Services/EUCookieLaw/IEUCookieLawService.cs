using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;

namespace Nop.Services.EUCookieLaw
{
    public interface IEUCookieLawService
    {
        Task<IList<ICookie>> GetAllCookiesAsync();

        Task<bool> IsCookiePurposeAllowedAsync<T>() where T : ICookiePurpose;
        Task<bool> IsCookieProviderAllowedAsync<T>() where T : ICookieProvider;
        Task<bool> IsCookieAllowedAsync<T>() where T : ICookie;

        Task<bool> IsCookiePurposeAllowedAsync<T>(T purpose) where T : ICookiePurpose;
        Task<bool> IsCookieProviderAllowedAsync<T>(T provider) where T : ICookieProvider;
        Task<bool> IsCookieAllowedAsync<T>(T cookie) where T : ICookie;
    }
}