using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;

namespace PlaywrightWDE.Global.Entry {


    public static class CommonEntryActions {

        private const int DefaultTimeout = 600_000;

        public static async Task EnterFieldAsync(
            IFrame frame,
            string selector,
            string value)
        {
            await CommonEntryHelpers.FillAsync(
                frame,
                selector,
                value,
                "✅ Field value entered");
        }
     
        public static async Task ClickExecuteAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.ExecuteButton.Selector,
                "✅ Clicked execute button");
        }

        public static async Task ClickMoreAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.MoreButton.Selector,
                "✅ Clicked more button");
        }

        public static async Task ClickChooseLayoutAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryMenuItems.ChooseLayoutItem.Selector,
                "✅ Clicked choose layout");
        }

        public static async Task ClickMenuAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.MenuButton.Selector,
                "✅ Clicked Menu");
        }

        public static async Task ClickSpreadsheetAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryMenuItems.SpreadSheetItem.Selector,
                "✅ Clicked Spreadsheet");
        }

        public static async Task HoverListAsync(IFrame frame)
        {
            await CommonEntryHelpers.HoverAsync(
            frame,
            role: AriaRole.Menuitem,
            name: CommonEntrySelectors.CommonEntryMenuItems.ListItem.Selector,
            log: "✅ Hovered List");
        }

        public static async Task HoverExportAsync(IFrame frame)
        {
            await CommonEntryHelpers.HoverAsync(
            frame,
            selector: CommonEntrySelectors.CommonEntryMenuItems.ExportItem.Selector,
            log: "✅ Hovered Export");
        }

        public static async Task<string> ClickOkAsync(
        IPage page,
        IFrame frame,
        CommonEntryHelpers.ReportType? reportType = null,
        string? siteCode = null)
        {
            var calendarValue = DailySalesSummaryReportEntrySelector
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
            var stagingFolder = Path.Combine(exportFolder, "staging");
            Directory.CreateDirectory(stagingFolder);

            var download = await page.RunAndWaitForDownloadAsync(
                () => CommonEntryHelpers.ClickAsync(
                    frame,
                    MasterReportsEntrySelector.MasterReportButtons.OkButton.Selector,
                    "✅ Clicked OK"),
                new() { Timeout = DefaultTimeout });

            var stagingFile = Path.Combine(stagingFolder, download.SuggestedFilename);
            await download.SaveAsAsync(stagingFile);
            Logger.Log($"✅ Downloaded file to staging: {stagingFile}");

            await WaitForFileReadyAsync(stagingFile);

            if (reportType == CommonEntryHelpers.ReportType.ArticleMaster)
            {
                var deletedRows = ExcelReportCleaner.CleanArticleMaster(stagingFile);

                if (deletedRows.Count > 0)
                    Logger.Log($"🧹 ArticleMaster Excel cleaned rows {string.Join(", ", deletedRows)} for site {siteCode}");
                else
                    Logger.Log($"🧹 ArticleMaster Excel: no rows deleted for site {siteCode}");
            }

            var finalFile = Path.Combine(exportFolder, download.SuggestedFilename);
            File.Copy(stagingFile, finalFile, overwrite: true);

            File.Delete(stagingFile);

            Logger.Log($"✅ File ready: {finalFile}");
            return finalFile;
        }

        private static async Task WaitForFileReadyAsync(string path, int retries = 20, int delayMs = 300)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        return;
                    }
                }
                catch (IOException)
                {
                    await Task.Delay(delayMs);
                }
            }

            throw new IOException($"File '{path}' is still locked after {retries * delayMs}ms.");
        }

    }
    
}



