using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SocionicTeamBuilder.DAL.EF
{
    public static class DbManager
    {
        public static string[]? GetBackupFiles(string backupDirectory)
        {
            if (!Directory.Exists(backupDirectory))
            {
                return null;
            }

            return Directory
                .GetFiles(backupDirectory, "*.bak", SearchOption.TopDirectoryOnly)
                .Select(x => Path.GetFileName(x))
                .ToArray();
        }

        public static string? BackupDB(string backupDirectory, string connectionString)
        {
            string filePath = string.Empty;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                string db = sqlConnection.Database.ToString();
                string cmd = $"BACKUP DATABASE @db TO DISK = @file";

                filePath = $"{backupDirectory}\\{db}-{DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss")}.bak";

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);

                SqlParameter dbParam = new SqlParameter("@db", db);
                SqlParameter fileParam = new SqlParameter("@file", filePath);

                sqlCommand.Parameters.Add(dbParam);
                sqlCommand.Parameters.Add(fileParam);

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    return null;
                }
            }

            return filePath;
        }

        public static bool RestoreDB(string backupFilePath, string connectionString)
        {
            if (!File.Exists(backupFilePath))
            {
                return false;
            }

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                string db = sqlConnection.Database.ToString();
                string singleUserCmd = string.Format($"ALTER DATABASE [{db}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand sqlCommand = new SqlCommand(singleUserCmd, sqlConnection);

                sqlConnection.Open();
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    return false;
                }

                string restoreDbCmd = $"USE MASTER RESTORE DATABASE @db FROM DISK = @file WITH REPLACE";
                sqlCommand = new SqlCommand(restoreDbCmd, sqlConnection);
                sqlCommand.Parameters.Add(new SqlParameter("@db", db));
                sqlCommand.Parameters.Add(new SqlParameter("@file", backupFilePath));

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    return false;
                }

                string multiUserCmd = string.Format($"ALTER DATABASE [{db}] SET MULTI_USER");
                sqlCommand = new SqlCommand(multiUserCmd, sqlConnection);

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
