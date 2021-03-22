using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nop.Extensions.MailerSend.Configuration;
using Nop.Services.Logging;

namespace Nop.Extensions.MailerSend.Clients
{
    public class MailerSendClient : IMailerSendClient
    {
        private const string API_URI = "https://api.mailersend.com/v1/email";
        private readonly string _apiToken;
        private readonly string _newCustomerTemplateId;
        private readonly ILogger _logger;
        private HttpClient _client;

        public MailerSendClient(HttpClient client, IOptions<MailerSendConfig> mailerSendConfig, ILogger logger)
        {
            _client = client;
            _apiToken = mailerSendConfig.Value.ApiToken;
            _newCustomerTemplateId = mailerSendConfig.Value.NewCustomerTemplateId;
            _logger = logger;

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiToken);
        }

        public async Task SendCustomerWelcomeEmailAsync(int customerId, string emailAddress, string name)
        {
            if (NoToken || string.IsNullOrWhiteSpace(_newCustomerTemplateId))
                return;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, API_URI);
                request.Content = JsonContent.Create(new { to = new[] { new { email = emailAddress, name = name } }, template_id = _newCustomerTemplateId });

                var result = await _client.SendAsync(request);

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                    return;

                if (result.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                {
                    await _logger.WarningAsync($"Failed to send customer welcome email [CustomerId={ customerId }]", new Exception(await result.Content.ReadAsStringAsync()));
                    return;
                }

                await _logger.WarningAsync($"Unexpected response status code while sending customer welcome email [CustomerId={ customerId }, StatusCode={ result.StatusCode.ToString() }]", new Exception(await result.Content.ReadAsStringAsync()));
            }
            catch (Exception e)
            {
                await _logger.WarningAsync($"Exception occurred while sending customer welcome email [CustomerId={ customerId }]", e);
            }
        }

        public bool NoToken { get { return string.IsNullOrWhiteSpace(_apiToken); } }
    }
}
