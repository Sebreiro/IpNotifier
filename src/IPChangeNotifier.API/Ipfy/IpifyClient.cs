using System.Net.Http;
using System.Threading.Tasks;

// ReSharper disable IdentifierTypo

namespace IPChangeNotifier.Clients.Ipfy
{
    public class IpifyClient : IIpRequestClient
    {
        private const string Address = "https://api.ipify.org";

        public async Task<string> GetIpAddress()
        {
            var client = new HttpClient();

            var response = await client.GetAsync(Address);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
