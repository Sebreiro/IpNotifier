using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPChangeNotifier.Application.Helpers;
using IPChangeNotifier.Clients;

namespace IPChangeNotifier.Application.Factory
{
    public class MessageJobFactory : IMessageJobFactory
    {
        private readonly IEnumerable<IIpRequestClient> _clients;

        public MessageJobFactory(IEnumerable<IIpRequestClient> clients)
        {
            _clients = clients;
        }

        public Func<Task<string>> GetJob()
        {
            async Task<string> Job()
            {
                foreach (var client in _clients)
                {
                    var ip = await client.GetIpAddress();
                    if (ip == null)
                    {
                        continue;
                    }

                    var message = MessageMakerHelper.CreateMessage(ip);
                    return message;
                }

                return null;
            }

            return Job;
        }
    }
}