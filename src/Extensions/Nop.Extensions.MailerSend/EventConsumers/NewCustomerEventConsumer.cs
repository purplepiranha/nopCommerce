using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Core.Events;
using Nop.Extensions.MailerSend.Clients;
using Nop.Extensions.MailerSend.Configuration;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Events;

namespace Nop.Extensions.MailerSend.EventConsumers
{
    public class NewCustomerEventConsumer : IConsumer<CustomerRegisteredEvent>
    {
        private readonly IMailerSendClient _mailerSendClient;
        private readonly IGenericAttributeService _genericAttributeService;

        public NewCustomerEventConsumer(IMailerSendClient mailerSendClient, IGenericAttributeService genericAttributeService)
        {
            _mailerSendClient = mailerSendClient;
            _genericAttributeService = genericAttributeService;
        }

        public async Task HandleEventAsync(CustomerRegisteredEvent eventMessage)
        {
            var forename = "Customer";

            var customer = eventMessage.Customer;
            var attributes = await _genericAttributeService.GetAttributesForEntityAsync(customer.Id, nameof(Customer));
            var forenameAttribute = attributes.Where(x => x.Key == "FirstName").FirstOrDefault();
            if (forenameAttribute != null)
                forename = forenameAttribute.Value;

            if (!customer.IsSystemAccount && customer.RegisteredInStoreId > 0)
            {
                await _mailerSendClient.SendCustomerWelcomeEmailAsync(customer.Id, customer.Email, forename);
            }
        }
    }
}
