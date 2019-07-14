using System.IO;
using IPChangeNotifier.Application.Config;
using IPChangeNotifier.MessageSender.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IPChangeNotifier.Start.Initialization
{
    public static class OptionsConfigurator
    {
        private static IConfigurationRoot Config()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config/appsettings.json", false, true)
                .Build();
        }

        public static IConfiguration Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions();
            var configurationRoot = Config();

            AddConfigParts(serviceCollection, configurationRoot);

            return configurationRoot;
        }

        private static void AddConfigParts(IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection.Configure<MessageSenderConfig>(configurationRoot.GetSection("messageSender"));
            serviceCollection.Configure<JobConfig>(configurationRoot.GetSection("jobConfig"));
        }
    }
}