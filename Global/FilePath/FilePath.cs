
using System;
using System.IO;

namespace PlaywrightWDE.Global.FilePath
{
    public static class FilePath
    {
        public static string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string ExtractedData => Path.Combine(Desktop, "ExtractedData");

        public static string GetDatedExportFolder(DateTime date)
        {
            var monthName = date.ToString("MMMM");        
            var folderName = date.ToString("MM-dd-yyyy");
            var exportPath = Path.Combine(ExtractedData, date.Year.ToString(), monthName, folderName);

            Directory.CreateDirectory(exportPath); 
            return exportPath;
        }

        public static string GetDatedLogsFolder(DateTime date)
        {
            var targetDate = date.AddDays(-1);

            var monthName = targetDate.ToString("MMMM");
            var folderName = targetDate.ToString("MM-dd-yyyy");
            var logsPath = Path.Combine(Desktop, "PlaywrightWDE", "Global", "Logs", targetDate.Year.ToString(), monthName, folderName, "logs.txt");

            Directory.CreateDirectory(logsPath);
            return logsPath;
        }

        public static string GetLogsFilePath(DateTime date)
        {
            var logsFolder = GetDatedLogsFolder(date);
            return Path.Combine(logsFolder, "logs.txt");
        }
    }
}
