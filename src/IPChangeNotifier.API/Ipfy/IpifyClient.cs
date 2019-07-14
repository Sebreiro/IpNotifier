using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// ReSharper disable IdentifierTypo

namespace IPChangeNotifier.Clients.Ipfy
{
    public class IpifyClient : IIpRequestClient
    {
        private const string Address = "https://api.ipify.org";

        private ILogger _logger;

        public IpifyClient(ILogger<IpifyClient> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetIpAddress()
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(Address);

                var result = await response.Content.ReadAsStringAsync();
                
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "IP Address request problem");
            }

            return null;
        }
    }
}