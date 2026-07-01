
using System;
using System.IO;
using PlaywrightWDE.Global.Selectors;

namespace PlaywrightWDE.FilePath
{
    public static class FilePath
    {
        public static string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

       public static string _baseLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Logs", "Extracted_ISR");

        public static string GetDatedExportFolder(DateTime date)
        {

            var monthName = date.ToString("MMMM");        
            var folderName = date.ToString("MM-dd-yyyy");
            var exportPath = Path.Combine(_baseLogPath, date.Year.ToString(), monthName, folderName);

            Directory.CreateDirectory(exportPath); 
            return exportPath;
        }

        public static string GetLogsFilePath()
        {
            var calendarField =
            CommonEntrySelectors.CommonEntryFields
                .CalendarField
                .DefaultValue;

            if (!DateTime.TryParseExact(
                    calendarField,
                    "dd.MM.yy",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out var extractedDate))
            {
                extractedDate = DateTime.Now;
            }
                    
            var exportFolder = GetDatedExportFolder(extractedDate);
            var exportPath =  Path.Combine(exportFolder, "logs.txt");
            return exportPath;
        }
       
    }
}
