using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Nop.Extensions.MailerSend.Configuration
{
    public class MailerSendConfig : IConfig
    {
        public string ApiToken { get; set; }
        public string NewCustomerTemplateId { get; set; }
    }
}
