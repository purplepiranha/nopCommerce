using System.Collections.Generic;
using Nop.Core.EUCookieLaw;

namespace Nop.Services.EUCookieLaw
{
    public interface ICookieProviderManager
    {
        IEnumerable<ICookieProvider> GetAllCookieProviders();
    }
}