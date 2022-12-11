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
        private readonly HttpClient _httpClient;

        public IpifyClient(ILogger<IpifyClient> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
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