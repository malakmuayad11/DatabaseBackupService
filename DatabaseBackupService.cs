using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace DatabaseBackupService
{
    public partial class DatabaseBackupService : ServiceBase
    {
        private string _ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private string _BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];
        private string _LogFolder = ConfigurationManager.AppSettings["LogFolder"];
        private string _BackupIntervalMinutes = ConfigurationManager.AppSettings["BackupIntervalMinutes"];
        private Timer _Timer;
        private double _BackupInterval = 0;
        private string _DatabaseName;

        public DatabaseBackupService()
        {
            InitializeComponent();
            _HandleConfigurations();
            _SetTimer();
        }

        /// <summary>
        /// This method handles the values returned from the App.config file. If the values are not
        /// provided in the App.config file, default values will be used.
        /// </summary>
        private void _HandleConfigurations()
        {
            // If the connection string to the desired database is not provide, stop the service.
            if (string.IsNullOrWhiteSpace(_ConnectionString))
            {
                clsGlobal.Log("Connection string is missing, and the service is stopped.", _LogFolder);
                this.Stop();
            }

            // Since the connection string is provided, we can exrtact the database's name safely.
            _DatabaseName = new SqlConnectionStringBuilder(ConfigurationManager
                .AppSettings["ConnectionString"]).InitialCatalog;

            if (string.IsNullOrWhiteSpace(_BackupFolder))
            {
                _BackupFolder = "C:\\DatabaseBackups";
                clsGlobal.Log("Backup Folder is empty and replaced with default path: " + _BackupFolder, _LogFolder);
            }

            if (string.IsNullOrWhiteSpace(_LogFolder))
            {
                _LogFolder = "C:\\DatabaseBackups\\Logs";
                clsGlobal.Log("Logs Folder is empty and replaced with default path: " + _LogFolder, _LogFolder);
            }

            if (string.IsNullOrWhiteSpace(_BackupIntervalMinutes))
            {
                _BackupIntervalMinutes = "60";
                clsGlobal.Log("Backup interval in minutes is empty and replaced with default interval: "
                    + _BackupIntervalMinutes + " minutes", _LogFolder);
            }
            // Ensure that folders exist.
            Directory.CreateDirectory(_BackupFolder);
            Directory.CreateDirectory(_LogFolder);
        }

        /// <summary>
        /// This method sets the timer that will back up the database based on the specified interval.
        /// </summary>
        private void _SetTimer()
        {
            _BackupInterval = clsGlobal.TimeInMilliseconds(Convert.ToDouble(_BackupIntervalMinutes));
            _Timer = new Timer(_BackupInterval);
            _Timer.Enabled = true;
            _Timer.AutoReset = true;
            _Timer.Elapsed += _Timer_Elapsed;
        }

        /// <summary>
        /// This method backs up the database when the interval is reached. If the backup is successful, 
        /// it logs a success message to the log file.
        /// </summary>
        private void _Timer_Elapsed(object sender, ElapsedEventArgs e) =>
            clsGlobal.BackupDatabase(_BackupFolder, _ConnectionString, _LogFolder, _DatabaseName);

        /// <summary>
        /// When the service is started, a log message is logged indicating the 
        /// start of the service. Furthermore, the timer starts to backup the database
        /// at the specified interval.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            clsGlobal.Log("Service Started.", _LogFolder);
            _Timer.Start();
        }

        /// <summary>
        /// This method stops the timer, close resources, and logs a message indicating
        /// that the service stopped, when the service is stopped.
        /// </summary>
        protected override void OnStop()
        {
            // From the constructor, if the connection string is not provided, the timer is not initialized.
            // Thus, we only stop the timer if the connection string is provided and the timer is initialized.
            if (_Timer != null)
            {
                _Timer.Stop();
                _Timer.Dispose();
            }
            clsGlobal.Log("Service Stopped.", _LogFolder);
        }

        /// <summary>
        /// This procedure works only in debugging mode to test the service before deployment.
        /// It starts the service, then stops it when the user presses enter.
        /// </summary>
        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine();
            OnStop();
        }
    }
}
