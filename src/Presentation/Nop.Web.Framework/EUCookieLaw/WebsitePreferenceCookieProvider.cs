using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;
using Nop.Core.Http;
using Nop.Services.EUCookieLaw;

namespace Nop.Web.Framework.EUCookieLaw
{
    public class WebsitePreferenceCookieProvider : BaseCookieProvider<PreferenceCookiePurpose>, ICookieProvider
    {
        public WebsitePreferenceCookieProvider() : base()
        {
        }

        public string SystemName => "EUCookieLaw.Providers.Website";
        public string Name => $"{ SystemName }.Name";

        public string Description => $"{ SystemName }.Description";

        public string PrivacyPolicyUrl => string.Empty;

        public override int Order => -1; // always show before others

        public string[] CookieNames => new string[] {
        };

        public async Task<bool> IsActiveAsync()
        {
            return true;
        }
    }
}
