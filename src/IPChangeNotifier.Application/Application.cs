using System;
using IPChangeNotifier.Application.Config;
using IPChangeNotifier.Application.Factory;
using IPChangeNotifier.MessageSender;
using IPChangeNotifier.Services.Schedule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IPChangeNotifier.Application
{
    public class Application
    {
        private readonly JobConfig _jobConfig;
        private readonly ILogger _logger;
        private readonly IMessageJobFactory _messageJobFactory;
        private readonly IMessageSenderService _messageSender;
        private readonly IScheduleService _scheduleService;

        public Application(
            ILogger<Application> logger,
            IMessageJobFactory messageJobFactory,
            IScheduleService scheduleService,
            IMessageSenderService messageSender,
            IOptions<JobConfig> jobConfig)
        {
            _logger = logger;
            _messageJobFactory = messageJobFactory;
            _scheduleService = scheduleService;
            _messageSender = messageSender;
            _jobConfig = jobConfig.Value;
        }

        public void Start()
        {
            _logger.LogInformation("Starting Ip Notifier");

            _scheduleService.AddRecurrentTask(Job, GetRepeatTime(), "IP Notifier");
        }

        private async void Job()
        {
            try
            {
                var action = _messageJobFactory.GetJob();

                var message = await action.Invoke();
                if (message == null)
                    return;

                _logger.LogInformation($"IP Message: {message}");
                await _messageSender.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Unhandled Exception; {ex}");
            }
        }

        private int GetRepeatTime()
        {
            var requestInterval = _jobConfig.RequestInterval;
            _logger.LogDebug($"IP Request interval is {requestInterval} min");
            return requestInterval;
        }
    }
}