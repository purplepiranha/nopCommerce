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
    public class WebsiteNecessaryCookieProvider : BaseCookieProvider<NecessaryCookiePurpose>, ICookieProvider
    {
        public WebsiteNecessaryCookieProvider() : base()
        {
        }

        public string SystemName => "EUCookieLaw.Providers.Website";
        public string Name => $"{ SystemName }.Name";

        public string Description => $"{ SystemName }.Description";

        public string PrivacyPolicyUrl => string.Empty;

        public override int Order => -1; // always show before others

        public string[] CookieNames => new string[] {
            NopCookieDefaults.Prefix + NopCookieDefaults.CustomerCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.AntiforgeryCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.SessionCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.TempDataCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.InstallationLanguageCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.ComparedProductsCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.RecentlyViewedProductsCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.AuthenticationCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.ExternalAuthenticationCookie,
            NopCookieDefaults.Prefix + NopCookieDefaults.IgnoreEuCookieLawWarning,
            NopCookieDefaults.Prefix + NopCookieDefaults.EuCookieConsentPurposesCookie
        };

        public async Task<bool> IsActiveAsync()
        {
            return true;
        }
    }
}
