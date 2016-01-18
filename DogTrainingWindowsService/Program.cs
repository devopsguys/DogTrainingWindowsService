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
