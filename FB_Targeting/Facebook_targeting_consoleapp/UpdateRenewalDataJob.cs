using Facebook_targeting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facebook_targeting_consoleapp
{
    public class UpdateRenewalDataJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            new FacebookService();
            return Task.CompletedTask;
        }
    }
}
