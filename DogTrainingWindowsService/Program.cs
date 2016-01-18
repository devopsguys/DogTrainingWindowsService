using System;
using System.ServiceProcess;

namespace DogTrainingWindowsService
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            if (args[0] == "/CreateEventLog")
            {
                DogTrainingWindowsService.CreateEventLogIfNotExists();
                return;
            }

            if (Environment.UserInteractive)
            {
                var service = new DogTrainingWindowsService();
                service.UserInteractiveStartAndStop();
            }
            else
            {
                var servicesToRun = new ServiceBase[] { new DogTrainingWindowsService() };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
