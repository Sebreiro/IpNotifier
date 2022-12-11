using System.Net;
using System.Net.Http;
using FluentAssertions;
using IPChangeNotifier.Clients.Ipfy;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

// ReSharper disable IdentifierTypo

namespace IPChangeNotifier.IntegrationTests.Ipfy
{
    public class IpfyTests
    {
        [Fact]
        public void ReqeustIpAsText()
        {
            var mockedHttpFactory = GetMockedHttpFactory();
            var client = new IpifyClient(NullLogger<IpifyClient>.Instance, mockedHttpFactory);

            var result = client.GetIpAddress().Result;

            IPAddress.TryParse(result, out _).Should().BeTrue();
        }

        private IHttpClientFactory GetMockedHttpFactory()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var client = new HttpClient();

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            IHttpClientFactory factory = mockFactory.Object;
            return factory;
        }
    }
}