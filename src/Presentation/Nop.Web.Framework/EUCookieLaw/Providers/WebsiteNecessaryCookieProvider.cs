using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;
using Nop.Web.Framework.EUCookieLaw.Purposes;

namespace Nop.Web.Framework.EUCookieLaw.Providers
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
    }
}
