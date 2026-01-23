// using Microsoft.Playwright;
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Selectors;
// using PlaywrightWDE.Global.FilePath;
// using PlaywrightWDE.Global.Logs;

// namespace PlaywrightWDE.VariableEntry
// {
//     public static class BIRAnalyticalReportVariableEntry
//     {
//         private const int DefaultTimeout = 120_000;

//         public static async Task EnterFieldAsync(IFrame frame, string fieldSelector, string value)
//         {
//             var field = frame.Locator(fieldSelector);
//             await field.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = DefaultTimeout });
//             await field.FillAsync(value);
//         }

//         public static async Task EnterDistributorSitesAsync(IFrame frame, string[] sites)
//         {
//             var allSites = string.Join(";", sites);
//             await EnterFieldAsync(
//                 frame,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.DistributorField.Selector,
//                 allSites
//             );
//         }

//         public static async Task ClickOkAsync(IFrame frame)
//         {
//             var okButton = frame.Locator(AnalyticalReportSelector.ExportExcelButton.Ok);
//             await okButton.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = DefaultTimeout });
//             await okButton.ClickAsync();
//         }

//         public static async Task SortByVerticalAsync(IFrame frame, string navSelector, string label)
//         {
//             await RightClickSidebarItemAsync(frame, navSelector, label);

//             var x0 = await WaitForPopupFrameAsync(frame, AnalyticalReportSelector.Popups.SapPopupMainX0);
//             await HoverAndClickAsync(x0, AnalyticalReportSelector.DrilldownMenuItems.ChangeDrilldown);

//             var x1 = await WaitForPopupFrameAsync(frame, AnalyticalReportSelector.Popups.SapPopupMainX1);
//             await HoverAndClickAsync(x1, AnalyticalReportSelector.DrilldownMenuItems.DrilldownBy);

//             var x2 = await WaitForPopupFrameAsync(frame, AnalyticalReportSelector.Popups.SapPopupMainX2);
//             await HoverAndClickAsync(x2, AnalyticalReportSelector.DrilldownMenuItems.Vertical);
//         }

//         public static async Task ExportToExcelAsync(IFrame frame)
//         {
//             var exportBtn = frame.Locator(AnalyticalReportSelector.ExportExcelButton.ExportToExcel);
//             await exportBtn.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15_000 });

//             var calendarField = AnalyticalReportSelector.DailySalesSummaryRepFields.CalendarField.DefaultValue;
//             if (!DateTime.TryParseExact(calendarField, "dd.MM.yy", null, System.Globalization.DateTimeStyles.None, out DateTime extractedDate))
//                 extractedDate = DateTime.Now;

//             var folderPath = FilePath.GetDatedExportFolder(extractedDate);
//             var logsFolderPath = FilePath.GetDatedLogsFolder(extractedDate);

//             var download = await frame.Page.RunAndWaitForDownloadAsync(() => exportBtn.ClickAsync());
//             var filePath = Path.Combine(folderPath, download.SuggestedFilename);
//             await download.SaveAsAsync(filePath);

//             var logFilePath = Path.Combine(logsFolderPath, Path.GetFileNameWithoutExtension(download.SuggestedFilename) + "_log.txt");
//             Logger.LogToFile($"Downloaded File: {download.SuggestedFilename}\nPath: {filePath}", logFilePath);

//             Logger.Log("✅ Exported to Excel");
//         }

//         private static async Task RightClickSidebarItemAsync(IFrame frame, string selector, string name)
//         {
//             var item = frame.Locator(selector);
//             await item.WaitForAsync(new() { State = WaitForSelectorState.Visible });
//             await item.ClickAsync(new() { Button = MouseButton.Right });
//             Logger.Log($"✅ Right-clicked {name}");
//         }

//         private static async Task<IFrame> WaitForPopupFrameAsync(IFrame parentFrame, string popupSelector, int timeout = 10_000)
//         {
//             var popupEl = await parentFrame.WaitForSelectorAsync(popupSelector, new() { Timeout = timeout })
//                           ?? throw new Exception($"❌ Popup iframe element '{popupSelector}' not found");

//             return await popupEl.ContentFrameAsync() 
//                    ?? throw new Exception($"❌ Popup iframe '{popupSelector}' not loaded");
//         }

//         private static async Task HoverAndClickAsync(IFrame frame, string text)
//         {
//             var item = frame.Locator($"tr:has-text('{text}')");
//             await item.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15_000 });
//             await item.HoverAsync();
//             await item.ClickAsync();
//             Logger.Log($"✅ Clicked {text}");
//         }
//     }
// }








using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.FilePath;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Helpers;


namespace PlaywrightWDE.Global.Entry
{
    public static class DailySalesSummaryReportEntry
    {

        /* -------------------- Fields -------------------- */

        public static async Task EnterFieldAsync(
            IFrame frame,
            string selector,
            string value)
        {
            await CommonEntryHelper.FillAsync(
                frame,
                selector,
                value,
                "✅ Field value entered");
        }

        public static async Task EnterDistributorSitesAsync(
            IFrame frame,
            string[] sites)
        {
            var allSites = string.Join(";", sites);

            await CommonEntryHelper.FillAsync(
                frame,
                DailySalesSummarySelector
                    .DailySalesSummaryRepFields
                    .DistributorField
                    .Selector,
                allSites,
                "✅ Distributor sites entered");
        }

        /* -------------------- Buttons -------------------- */

        public static async Task ClickOkAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                DailySalesSummarySelector.ExportExcelButton.Ok,
                "✅ Clicked OK");
        }

        /* -------------------- Drilldown / Sort -------------------- */

        public static async Task SortByVerticalAsync(
            IFrame frame,
            string navSelector,
            string label)
        {
            await RightClickSidebarItemAsync(frame, navSelector, label);

            var x0 = await WaitForPopupFrameAsync(
                frame,
                DailySalesSummarySelector.Popups.SapPopupMainX0);

            await HoverAndClickAsync(
                x0,
                DailySalesSummarySelector.DrilldownMenuItems.ChangeDrilldown);

            var x1 = await WaitForPopupFrameAsync(
                frame,
                DailySalesSummarySelector.Popups.SapPopupMainX1);

            await HoverAndClickAsync(
                x1,
                DailySalesSummarySelector.DrilldownMenuItems.DrilldownBy);

            var x2 = await WaitForPopupFrameAsync(
                frame,
                DailySalesSummarySelector.Popups.SapPopupMainX2);

            await HoverAndClickAsync(
                x2,
                DailySalesSummarySelector.DrilldownMenuItems.Vertical);
        }

        /* -------------------- Export -------------------- */

        public static async Task<string> ExportToExcelAsync(IFrame frame)
        {
            var calendarField =
                DailySalesSummarySelector
                    .DailySalesSummaryRepFields
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

            var exportFolder = FilePath.FilePath.GetDatedExportFolder(extractedDate);
            var logsFolder = FilePath.FilePath.GetDatedLogsFolder(extractedDate);

            var download = await frame.Page.RunAndWaitForDownloadAsync(
                () => CommonEntryHelper.ClickAsync(
                    frame,
                    DailySalesSummarySelector.ExportExcelButton.ExportToExcel,
                    "✅ Clicked Export to Excel"));

            var filePath = Path.Combine(exportFolder, download.SuggestedFilename);
            await download.SaveAsAsync(filePath);

            var logFilePath = Path.Combine(
                logsFolder,
                Path.GetFileNameWithoutExtension(download.SuggestedFilename) + "_log.txt");

            Logger.LogToFile(
                $"Downloaded File: {download.SuggestedFilename}\nPath: {filePath}",
                logFilePath);

            Logger.Log($"✅ Exported to Excel: {filePath}");
            
            return filePath; 
        }

        /* -------------------- SAP Helpers -------------------- */

        private static async Task RightClickSidebarItemAsync(
            IFrame frame,
            string selector,
            string name)
        {
            var item = frame.Locator(selector);

            await item.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible
            });

            await item.ClickAsync(new()
            {
                Button = MouseButton.Right
            });

            Logger.Log($"✅ Right-clicked {name}");
        }

        private static async Task<IFrame> WaitForPopupFrameAsync(
            IFrame parentFrame,
            string popupSelector,
            int timeout = 10_000)
        {
            var popupEl =
                await parentFrame.WaitForSelectorAsync(
                    popupSelector,
                    new() { Timeout = timeout })
                ?? throw new Exception($"❌ Popup iframe '{popupSelector}' not found");

            return await popupEl.ContentFrameAsync()
                   ?? throw new Exception($"❌ Popup iframe '{popupSelector}' not loaded");
        }

        private static async Task HoverAndClickAsync(
            IFrame frame,
            string text)
        {
            var item = frame.Locator($"tr:has-text('{text}')");

            await item.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15_000
            });

            await item.HoverAsync();
            await item.ClickAsync();

            Logger.Log($"✅ Clicked {text}");
        }
    }
}
