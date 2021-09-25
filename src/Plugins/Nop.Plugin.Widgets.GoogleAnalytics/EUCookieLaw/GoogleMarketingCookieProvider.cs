using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;
using Nop.Core.Infrastructure;
using Nop.Services.EUCookieLaw;

namespace Nop.Plugin.Widgets.GoogleAnalytics.EUCookieLaw
{
    public class GoogleMarketingCookieProvider : BaseWidgetPluginCookieProvider<MarketingCookiePurpose>, ICookieProvider
    {
        private readonly GoogleAnalyticsSettings _googleAnalyticsSettings;
        public GoogleMarketingCookieProvider() : base()
        {
            _googleAnalyticsSettings = EngineContext.Current.Resolve<GoogleAnalyticsSettings>();
        }

        public string SystemName => "Plugins.Widgets.GoogleAnalytics.EUCookieLaw.Providers.Google.Marketing";

        public string Name => "Google";

        public string Description => "Description here! Do we need to use resources?";

        public string PrivacyPolicyUrl => "https://policies.google.com/privacy";

        public string[] CookieNames => new string[] {
            "_ga",
            "_gid",
            $"_ga_{_googleAnalyticsSettings.GoogleId}",
            $"_gac_gb_{_googleAnalyticsSettings.GoogleId}",
            "_gat",
            "AMP_TOKEN",
            $"_gac_{_googleAnalyticsSettings.GoogleId}"
        };

        public async override Task<bool> IsActiveAsync()
        {
            return await base._widgetPluginManager.IsPluginActiveAsync("Widgets.GoogleAnalytics");
        }
    }
}
