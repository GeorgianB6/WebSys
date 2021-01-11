using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Topshelf;
using Facebook_targeting_consoleapp;

namespace Facebook_targeting
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                HostFactory.Run(serviceConfig =>
                {
                    serviceConfig.Service<WindowsService>(serviceInstance =>
                    {
                        serviceInstance.ConstructUsing(() => new WindowsService());
                        serviceInstance.WhenStarted(execute => execute.BuildAndStartJob());
                        serviceInstance.WhenStopped(execute => execute.Stop());
                    });

                    serviceConfig.SetServiceName("Test Service");
                    serviceConfig.SetDisplayName("Test Service");
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
