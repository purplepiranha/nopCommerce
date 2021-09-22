using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.EUCookieLaw
{
    public interface ICookieProvider
    {
        string SystemName { get; }
        string Name { get; }
        string Description { get; }
        string PrivacyPolicyUrl { get; }
        ICookiePurpose CookiePurpose { get; }
        int Order { get; }

        Task<bool> IsActiveAsync();
    }
}
