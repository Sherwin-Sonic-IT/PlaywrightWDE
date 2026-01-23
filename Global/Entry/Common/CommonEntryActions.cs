using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;

namespace PlaywrightWDE.Global.Entry {


    public static class CommonEntryActions {

        // Default timeout in milliseconds (10 minutes)
        private const int DefaultTimeout = 600_000;

        public static async Task EnterCustomFileNameAsync(
            IFrame frame,
            string fileName)
        {
            await CommonEntryHelper.FillAsync(
                frame,
                CommonEntrySelectors.CommonEntryFields.FileNameField.Selector,
                fileName,
                $"✅ File name changed to '{fileName}'");
        }

        public static async Task ClickExecuteAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.ExecuteButton.Selector,
                "✅ Clicked Execute button");
        }

        public static async Task ClickMoreAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.MoreButton.Selector,
                "✅ Clicked More button");
        }

        public static async Task ClickChooseLayoutAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryMenuItems.ChooseLayoutItem.Selector,
                "✅ Clicked Choose Layout");
        }

        public static async Task ClickMenuAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.MenuButton.Selector,
                "✅ Clicked Menu");
        }

        public static async Task ClickSpreadsheetAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryMenuItems.SpreadSheetItem.Selector,
                "✅ Clicked Spreadsheet");
        }

        public static async Task HoverListAsync(IFrame frame)
        {
            await CommonEntryHelper.HoverAsync(
            frame,
            role: AriaRole.Menuitem,
            name: CommonEntrySelectors.CommonEntryMenuItems.ListItem.Selector,
            log: "✅ Hovered List");
        }

         public static async Task HoverExportAsync(IFrame frame)
        {
            await CommonEntryHelper.HoverAsync(
            frame,
            selector: CommonEntrySelectors.CommonEntryMenuItems.ExportItem.Selector,
            log: "✅ Hovered Export");
        }

        // public static async Task HoverExportAsync(IFrame frame)
        // {
        //     await CommonEntryHelper.HoverAsync(
        //     frame,
        //     selector: CommonEntrySelectors.CommonEntryMenuItems.ExportItem.Selector,
        //     log: "✅ Hovered Export",
        //     postHoverDelayMs: 200);
        // }

        public static async Task<string> ClickOkAsync(
            IPage page,
            IFrame frame)
        {
            var calendarValue =
                DailySalesSummarySelector
                    .DailySalesSummaryRepFields
                    .CalendarField
                    .DefaultValue;

            if (!DateTime.TryParseExact(
                    calendarValue,
                    "dd.MM.yy",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out var date))
            {
                date = DateTime.Now;
                Logger.Log("⚠️ Calendar parse failed, using today");
            }

            var exportFolder = FilePath.FilePath.GetDatedExportFolder(date);
            var logsFolder = FilePath.FilePath.GetDatedLogsFolder(date);

            var download = await page.RunAndWaitForDownloadAsync(
                () => CommonEntryHelper.ClickAsync(
                    frame,
                    CommonEntrySelectors.CommonEntryButtons.OkButton.Selector,
                    "✅ Clicked OK"),
                new() { Timeout = DefaultTimeout });

            var filePath = Path.Combine(exportFolder, download.SuggestedFilename);
            await download.SaveAsAsync(filePath);

            var logFilePath = Path.Combine(
                logsFolder,
                Path.GetFileNameWithoutExtension(download.SuggestedFilename) + "_log.txt");

            await File.WriteAllTextAsync(
                logFilePath,
                $"Downloaded File: {download.SuggestedFilename}\nPath: {filePath}");

            Logger.Log($"✅ Downloaded file: {filePath}");
            return filePath;
        }

    }

}
