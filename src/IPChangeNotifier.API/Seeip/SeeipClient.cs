using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IPChangeNotifier.Clients.Seeip;

/// <summary>
/// Client for seeip.org
/// </summary>
public class SeeipClient : IIpRequestClient
{
    private const string ApiAddress = "https://ip.seeip.org/jsonip";
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public SeeipClient(ILogger<SeeipClient> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string> GetIpAddress()
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiAddress);

            var resultJson = await response.Content.ReadAsStringAsync();

            _logger.LogDebug($"SeeIp json result: {resultJson}");

            var dataDefinition = new { Ip = string.Empty };
            var data = JsonConvert.DeserializeAnonymousType(resultJson, dataDefinition);

            var ip = data?.Ip;

            _logger.LogDebug($"SeeIp. Deserialized IP: {ip} ");

            return data?.Ip;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "IP Address request problem");
            return null;
        }
    }
}