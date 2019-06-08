using System;
using System.Threading.Tasks;
using IPChangeNotifier.Application.Helpers;
using IPChangeNotifier.Clients;

namespace IPChangeNotifier.Application.Factory
{
    public class MessageJobFactory : IMessageJobFactory
    {
        private readonly IIpRequestClient _client;

        public MessageJobFactory(IIpRequestClient client)
        {
            _client = client;
        }

        public Func<Task<string>> GetJob()
        {
            async Task<string> Job()
            {
                var ip = await _client.GetIpAddress();
                if (ip == null) return null;

                var message = MessageMakerHelper.CreateMessage(ip);
                return message;
            }

            return Job;
        }
    }
}