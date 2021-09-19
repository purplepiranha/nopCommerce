using System.Collections.Generic;
using FluentMigrator;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Data.Migrations;
using Nop.Services.Localization;

namespace Nop.Web.Framework.Migrations.EUCookieLaw.V1
{
    [NopMigration("2021-09-19 00:00:00", "EUCookieLaw.V1")]
    [SkipMigrationOnInstall]
    public class LocalizationMigration : MigrationBase
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            if (!DataSettingsManager.IsDatabaseInstalled())
                return;

            //do not use DI, because it produces exception on the installation process
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            //use localizationService to add, update and delete localization resources
            localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                ["EUCookieLaw.Title"] = "Our use of cookies",
                ["EUCookieLaw.Description3"] = "We use necessary cookies to make our site work. We'd also like to set analytics cookies that help us make improvements by measuring how you use the site and marketing cookies to show you more relevent content on social media and other websites. These will be set only if you accept.",
                ["EUCookieLaw.Purposes.Necessary.Title"] = "Necessary Cookies",
                ["EUCookieLaw.Purposes.Necessary.Description"] = "These are required for the basic functionality of our website. e.g. for remembering items that you add to the shopping cart.",
                ["EUCookieLaw.Purposes.Analytical.Title"] = "Analytical Cookies",
                ["EUCookieLaw.Purposes.Analytical.Description"] = "Allow us to gather statistics about how people use our website and where traffic to our website comes from.",
                ["EUCookieLaw.Purposes.Marketing.Title"] = "Marketing Cookies",
                ["EUCookieLaw.Purposes.Marketing.Description"] = "Help to provide more relevent content and advertisments on social media and other websites.",
                ["EUCookieLaw.AcceptNecessary"] = "Accept Necessary",
                ["EUCookieLaw.AcceptAll"] = "Accept All",
                ["EUCookieLaw.AcceptSelected"] = "Accept Selected",
            });
        }

        /// <summary>Collects the DOWN migration expressions</summary>
        public override void Down()
        {
            //add the downgrade logic if necessary 
        }
    }
}
