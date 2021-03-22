using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Extensions.MailerSend.Clients;

namespace Nop.Extensions.MailerSend.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 101;

        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<IMailerSendClient, MailerSendClient>();
            services.AddHttpClient<IMailerSendClient, MailerSendClient>();
        }
    }
}
