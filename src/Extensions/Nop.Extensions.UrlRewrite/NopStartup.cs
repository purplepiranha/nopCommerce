using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core;
using Nop.Core.Infrastructure;

namespace Nop.Extensions.UrlRewrite
{
    public class NopStartup : INopStartup
    {
        private INopFileProvider _fileProvider;

        public NopStartup()
        {
            _fileProvider = EngineContext.Current.Resolve<INopFileProvider>();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 101;

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Configure(IApplicationBuilder application)
        {
            var settingsPath = _fileProvider.MapPath("~/App_Data");
            var rewriteFilePath = _fileProvider.Combine(settingsPath, "RewriteRules.xml");
            if (_fileProvider.FileExists(rewriteFilePath))
            {
                using (StreamReader iisUrlRewriteStreamReader = File.OpenText(rewriteFilePath))
                {
                    var options = new RewriteOptions()
                    .AddIISUrlRewrite(iisUrlRewriteStreamReader);
                    application.UseRewriter(options);
                }
            }
        }

        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
