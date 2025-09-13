using System;
using System.ServiceProcess;

namespace DatabaseBackupService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive) // Debug mode
            {
                Console.WriteLine("Running in console mode.");
                DatabaseBackupService service = new DatabaseBackupService();
                service.StartInConsole();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new DatabaseBackupService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
