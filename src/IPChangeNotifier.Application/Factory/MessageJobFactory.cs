using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPChangeNotifier.Application.Helpers;
using IPChangeNotifier.Clients;

namespace IPChangeNotifier.Application.Factory
{
    public class MessageJobFactory: IMessageJobFactory
    {

        private readonly IIpRequestClient _client;

        public MessageJobFactory(IIpRequestClient client)
        {
            _client = client;
        }

        public Func<Task<string>> GetJob()
        {
            Func<Task<string>> job = async () =>
            {
                var ip = await _client.GetIpAddress();
                if (ip == null)
                    return null;

                var message = MessageMakerHelper.CreateMessage(ip);
                return message;
            };

            return job;
        }
    }
}
