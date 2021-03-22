using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Extensions.MailerSend.Configuration;

namespace Nop.Extensions.MailerSend.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 101;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailerSendConfig>(configuration.GetSection("MailerSendConfig"));
        }
    }
}
