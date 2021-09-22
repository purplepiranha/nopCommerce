using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.EUCookieLaw;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.EUCookieLaw.Purposes;
using Nop.Core.EUCookieLaw;

namespace Nop.Plugin.Widgets.GoogleAnalytics.EUCookieLaw.Providers
{
    public class GoogleAnalyticsCookieProvider : BaseWidgetPluginCookieProvider<AnalyticalCookiePurpose>, ICookieProvider
    {
        public GoogleAnalyticsCookieProvider() : base()
        {
        }

        public string SystemName => "Plugins.Widgets.GoogleAnalytics.EUCookieLaw.Providers.Google.Analytics";

        public string Name => "Google";

        public string Description => "Description here! Do we need to use resources?";

        public string PrivacyPolicyUrl => "https://policies.google.com/privacy";

        public async override Task<bool> IsActiveAsync()
        {
          return await base._widgetPluginManager.IsPluginActiveAsync("Widgets.GoogleAnalytics");
        }
    }
}
