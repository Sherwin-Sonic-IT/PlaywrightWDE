
using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.FilePath;
using PlaywrightWDE.Logs;
using PlaywrightWDE.Global.Helpers;
using System.Globalization;


namespace PlaywrightWDE.Global.Entry
{
    public static class ManualPJPUploadEntryActions
    {

        public static async Task SelectRadioButtonAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
               frame,
               ManualPJPUploadEntrySelector.ManualPJPUploadRadioButtons.Download.Selector,
               "✅ Download radio button clicked");
        }

        public static async Task ClickSubmitAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                ManualPJPUploadEntrySelector.ManualPJPUploadButtons.Submit.Selector,
                "✅ Clicked Submit");
        }

        public static async Task<string> ClickDownloadAsync(IPage page, IFrame frame)
        {
            var calendarField =
                CommonEntrySelectors.CommonEntryFields
                    .CalendarField
                    .DefaultValue;

                // var calendarField =
                // DailySalesSummaryReportEntrySelector
                //     .DailySalesSummaryRepFields
                //     .CalendarField
                //     .DefaultValue;

            if (!DateTime.TryParseExact(
                    calendarField,
                    "dd.MM.yy",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out var extractedDate))
            {
                extractedDate = DateTime.Now;
            }

            var exportFolder = FilePath.FilePath.GetDatedExportFolder(extractedDate);

            var downloadTask = page.WaitForDownloadAsync();

            await CommonEntryHelpers.ClickAsync(
                frame,
                ManualPJPUploadEntrySelector.ManualPJPUploadButtons.Download.Selector,
                "✅ Clicked Download"
            );

            var download = await downloadTask;

            string extension = Path.GetExtension(download.SuggestedFilename);
            string isrFileName = $"ISR_PJP_{extractedDate:yyyy-MM-dd}{extension}";
            string isrPath = Path.Combine(exportFolder, isrFileName);

            await download.SaveAsAsync(isrPath);
            Logger.Log($"✅ ISR file downloaded: {isrPath}");

            // Convert ISR template to API template
            string apiFilePath = ConvertToAPITemplate.ConvertToApiTemplate(isrPath, exportFolder);
            Logger.Log($"✅ API template generated: {apiFilePath}");

            return apiFilePath;
        }

        // public static async Task<string> ClickDownloadAsync(IPage page, IFrame frame)
        // {
        //     var exportFolder = FilePath.FilePath.GetDatedExportFolder(DateTime.Now);

        //     string datePart = DateTime.Now.ToString("yyyy-MM-dd");
            
        //     var downloadTask = page.WaitForDownloadAsync();

        //     await CommonEntryHelpers.ClickAsync(
        //         frame,
        //         ManualPJPUploadEntrySelector.ManualPJPUploadButtons.Download.Selector,
        //         "✅ Clicked Download"
        //     );

        //     var download = await downloadTask;

        //     string extension = Path.GetExtension(download.SuggestedFilename);

        //     string newFileName = $"PjpPlanUploaderTemplate__{datePart}{extension}";

        //     var downloadPath = Path.Combine(exportFolder, newFileName);

        //     await download.SaveAsAsync(downloadPath);

        //     Logger.Log($"✅ Download completed: {downloadPath}");

        //     return downloadPath;
        // }

    }
}

