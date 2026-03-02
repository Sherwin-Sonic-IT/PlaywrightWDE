
using System;
using System.IO;

namespace PlaywrightWDE.Global.Logs
{
    public static class Logger
    {
        private static readonly object _lock = new();

        public static void Log(string message)
        {
            var logFilePath = FilePath.FilePath.GetLogsFilePath(DateTime.Now);

            LogToFile(message, logFilePath);
        }

        public static void LogToFile(string message, string filePath)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(line);

            lock (_lock)
            {
                var folder = Path.GetDirectoryName(filePath)!;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                File.AppendAllText(filePath, line + Environment.NewLine);
            }
        }
    }
}
