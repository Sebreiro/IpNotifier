using System;

namespace IPChangeNotifier.Services.Schedule
{
    public interface IScheduleService
    {
        void AddRecurrentTask(Action job, int repeatTime, string jobName);

        void AddOneTimeTask(Action job, string jobName);
    }
}