using System;
using System.IO;

namespace PlaywrightWDE.Global.FilePath
{
    public static class FilePath
    {
        public static string Desktop =>
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string ExtractedData =>
            Path.Combine(Desktop, "ExtractedData");

       public static string GetDatedExportFolder(DateTime date)
        {
            var folderName = date.ToString("yyyy-MM-dd");
            var path = Path.Combine(ExtractedData, date.Year.ToString(), folderName);

            Directory.CreateDirectory(path);
            return path;
        }

         public static string GetDatedLogsFolder(DateTime date)
        {
            var logsPath = Path.Combine(GetDatedExportFolder(date), "Logs");
            Directory.CreateDirectory(logsPath);
            return logsPath;
        }

        public static string LogsText =>
            Path.Combine(Desktop, "PlaywrightWDE", "logs.txt");

    }
}


