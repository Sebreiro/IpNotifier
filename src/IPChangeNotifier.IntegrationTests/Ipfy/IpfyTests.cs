using System.Net;
using FluentAssertions;
using IPChangeNotifier.Clients.Ipfy;
using Xunit;

// ReSharper disable IdentifierTypo

namespace IPChangeNotifier.IntegrationTests.Ipfy
{
    public class IpfyTests
    {
        [Fact]
        public void ReqeustIpAsText()
        {
            var client = new IpifyClient();

            var result = client.GetIpAddress().Result;

            IPAddress.TryParse(result, out _).Should().BeTrue();
        }
    }
}