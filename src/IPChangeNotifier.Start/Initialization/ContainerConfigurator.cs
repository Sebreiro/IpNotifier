using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IPChangeNotifier.Application.Factory;
using IPChangeNotifier.Clients;
using IPChangeNotifier.Clients.Ipfy;
using IPChangeNotifier.MessageSender;
using IPChangeNotifier.Services.Schedule;
using Microsoft.Extensions.DependencyInjection;

namespace IPChangeNotifier.Start.Initialization
{
    public static class ContainerConfigurator
    {
        public static IServiceProvider Configure(IServiceCollection serviceCollection)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);

            Register(containerBuilder);

            var container = containerBuilder.Build();

            var serviceProvider = new AutofacServiceProvider(container);
            return serviceProvider;
        }

        private static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<ScheduleService>().As<IScheduleService>();
            builder.RegisterType<MessageSenderService>().As<IMessageSenderService>();
            builder.RegisterType<MessageJobFactory>().As<IMessageJobFactory>();
            builder.RegisterType<Application.Application>().As<Application.Application>();
            builder.RegisterType<IpifyClient>().As<IIpRequestClient>();
        }
    }
}