using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IPChangeNotifier.MessageSender.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IPChangeNotifier.MessageSender
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly MessageSenderConfig _config;
        private readonly ILogger _logger;
        // private readonly IHttpClientFactory

        public MessageSenderService(ILogger<MessageSenderService> logger, IOptionsSnapshot<MessageSenderConfig> config)
        {
            _logger = logger;
            _config = config.Value;
            CheckConfig(_config);
        }

        public async Task Send(string message)
        {
            if (message == null)
            {
                _logger.LogError("Message is null");
                return;
            }

            var requestUrl = _config.Url;
            var client = new HttpClient();

            var httpContent = new StringContent($"{{channelName:\"{_config.ChannelName}\", message:\"{message}\"}}", Encoding.UTF8, "application/json");
            try
            {
                var result = await client.PostAsync(requestUrl, httpContent);
                if (result.StatusCode != HttpStatusCode.OK)
                    _logger.LogError($"{result}");
            }
            catch (Exception ex) when (ex.InnerException is SocketException)
            {
                _logger.LogError($"RequestUrl: {requestUrl};{Environment.NewLine}RequestMessage: {message}{Environment.NewLine}Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        private bool CheckConfig(MessageSenderConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.Url))
                throw new InvalidOperationException("MessageSenderConfig Url is missing");

            if (string.IsNullOrWhiteSpace(config.ChannelName))
                throw new InvalidOperationException("MessageSenderConfig ChannelName is missing");

            return true;
        }
    }
}