using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;

namespace Nop.Services.EUCookieLaw
{
    public interface ICookieRegistrar
    {
        IEnumerable<ICookieProvider> GetAllCookieProviders();
        IEnumerable<ICookiePurpose> GetAllCookiePurposes();
    }
}