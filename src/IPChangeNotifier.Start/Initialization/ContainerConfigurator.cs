using System;
using IPChangeNotifier.Application.Factory;
using IPChangeNotifier.Clients;
using IPChangeNotifier.Clients.Ipfy;
using IPChangeNotifier.Clients.Seeip;
using IPChangeNotifier.MessageSender;
using IPChangeNotifier.Services.Schedule;
using Microsoft.Extensions.DependencyInjection;

namespace IPChangeNotifier.Start.Initialization
{
    public static class ContainerConfigurator
    {
        public static IServiceProvider Configure(IServiceCollection serviceCollection)
        {
            Register(serviceCollection);

            serviceCollection.AddHttpClient();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IScheduleService, ScheduleService>();
            serviceCollection.AddTransient<IMessageSenderService, MessageSenderService>();
            serviceCollection.AddTransient<IMessageJobFactory, MessageJobFactory>();
            serviceCollection.AddTransient<Application.Application>();

            serviceCollection.AddTransient<IIpRequestClient, IpifyClient>();
            serviceCollection.AddTransient<IIpRequestClient, SeeipClient>();
        }
    }
}