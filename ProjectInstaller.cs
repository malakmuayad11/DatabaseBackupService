using System.ComponentModel;
using System.ServiceProcess;

namespace DatabaseBackupService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.NetworkService
            };

            // Service configuration
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "DatabaseBackupService",
                DisplayName = "Database Backup Service",
                StartType = ServiceStartMode.Automatic,
                Description = "A Windows service that backs up a specified database at each specified interval.",
                ServicesDependedOn = new string[] { "MSSQLSERVER", "EventLog", "RpcSs" } // Dependencies
            };
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
