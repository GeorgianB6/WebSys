using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook_targeting_consoleapp
{
    class WindowsService
    {
        private IScheduler _scheduler;

        public bool BuildAndStartJob()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
                var job = JobBuilder.Create<UpdateRenewalDataJob>()
                    .WithIdentity("UpdateRenewalDataJob")
                    .StoreDurably()
                    .Build();
                var cronString = "0 0/1 * 1/1 * ? *";
                if (!string.IsNullOrEmpty(cronString))
                {
                    var trigger = TriggerBuilder.Create()
                        .WithIdentity("UpdateRenewalDataJobTrigger")
                        .WithCronSchedule(cronString)
                        .Build();

                    _scheduler.ScheduleJob(job, trigger);
                }
                else
                {
                    _scheduler.AddJob(job, true);
                    _scheduler.TriggerJob(job.Key);
                }
            

            _scheduler.Start();
            return true;
        }

        public bool Stop()
        {
            _scheduler.Shutdown(true);
            return true;
        }
    }
}
