using System;
using System.Data.SqlClient;
using System.IO;

namespace DatabaseBackupService
{
    public static class clsGlobal
    {
        /// <summary>
        /// This method return the provided minutes converted to milliseconds.
        /// </summary>
        /// <param name="Minutes">The minutes interval to be converted.</param>
        /// <returns>The converted minutes in milliseconds.</returns>
        public static double TimeInMilliseconds(double Minutes) => Minutes * 60 * 1000;

        /// <summary>
        /// This method logs a certain message in the log file of this service.
        /// </summary>
        /// <param name="Message">The message to be logged in the log file.</param>
        /// <param name="LogFolder">The log folder to log messages to.</param>
        public static void Log(string Message, string LogFolder)
        {
            string LogFilePath = Path.Combine(LogFolder, "BackupServiceLogs.txt");
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {Message}\n";

            File.AppendAllText(LogFilePath, logMessage);

            if (Environment.UserInteractive) // If in the debug mode, show the log message on the console.
                Console.WriteLine(logMessage);
        }

        /// <summary>
        /// This method backs up a specified database to the specified backup file destination.
        /// Messages will be logged to the log file, indicating the success or failure of backup.
        /// </summary>
        /// <param name="BackupDestination">The backup file path destination.</param>
        /// <param name="ConnectionString">The connection string of the database to backup.</param>
        /// <param name="LogFolder">The log folder to log messages to.</param>
        /// <param name="DatabaseName">The database to backup.</param>
        public static void BackupDatabase(string BackupDestination,
            string ConnectionString, string LogFolder, string DatabaseName)
        {
            string BackupFilePath = BackupDestination + $"\\Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = $@"BACKUP DATABASE [{DatabaseName}]
                    TO DISK = '{BackupFilePath}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        rowsEffected = command.ExecuteNonQuery();
                        if (rowsEffected == -1) // If the backup seccess, -1 will be returned.
                            Log($"Database backup successful: {BackupFilePath}", LogFolder);
                    }
                }
            }
            catch (SqlException)
            {
                Log("Error during backup: Network-related or instance-specific error occurred" +
                    " while establishing a connection to SQL Server.", LogFolder);
            }
        }
    }
}
