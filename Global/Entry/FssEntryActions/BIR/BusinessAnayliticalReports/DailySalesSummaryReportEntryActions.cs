
using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.FilePath;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Helpers;
using System.Globalization;


namespace PlaywrightWDE.Global.Entry
{
    public static class DailySalesSummaryReportEntryActions
    {

        /* -------------------- Fields -------------------- */

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

        public static async Task EnterDistributorSitesAsync(
            IFrame frame,
            string[] sites)
        {
            var allSites = string.Join(";", sites);

            await CommonEntryHelpers.FillAsync(
                frame,
                DailySalesSummaryReportEntrySelector
                    .DailySalesSummaryRepFields
                    .DistributorField
                    .Selector,
                allSites,
                "✅ Distributor sites entered");
        }

        /* -------------------- Buttons -------------------- */

        public static async Task ClickOkAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                DailySalesSummaryReportEntrySelector.ExportExcelButton.Ok,
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
                DailySalesSummaryReportEntrySelector.Popups.SapPopupMainX0);

            await HoverAndClickAsync(
                x0,
                DailySalesSummaryReportEntrySelector.DrilldownMenuItems.ChangeDrilldown);

            var x1 = await WaitForPopupFrameAsync(
                frame,
                DailySalesSummaryReportEntrySelector.Popups.SapPopupMainX1);

            await HoverAndClickAsync(
                x1,
                DailySalesSummaryReportEntrySelector.DrilldownMenuItems.DrilldownBy);

            var x2 = await WaitForPopupFrameAsync(
                frame,
                DailySalesSummaryReportEntrySelector.Popups.SapPopupMainX2);

            await HoverAndClickAsync(
                x2,
                DailySalesSummaryReportEntrySelector.DrilldownMenuItems.Vertical);
        }

        /* -------------------- Export -------------------- */

        public static async Task<string> ExportToExcelAsync(IFrame frame)
        {
            var calendarField =
                DailySalesSummaryReportEntrySelector
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

            var download = await frame.Page.RunAndWaitForDownloadAsync(
                () => CommonEntryHelpers.ClickAsync(
                    frame,
                    DailySalesSummaryReportEntrySelector.ExportExcelButton.ExportToExcel,
                    "✅ Clicked Export to Excel"));

            var tempFilePath = Path.Combine(exportFolder, Path.GetRandomFileName() + ".xls");
            await download.SaveAsAsync(tempFilePath);

          var xlsxFileName = $"DAILY_INVOICE_REPORT_{extractedDate:yyyy-MM-dd}.xlsx";

            var xlsxFilePath = Path.Combine(exportFolder, xlsxFileName);

            ConvertHTMLXlsToXlsx.ConvertDailySalesSummaryToXlsx(tempFilePath, xlsxFilePath);

            Logger.Log($"✅ Downloaded File: {xlsxFilePath}");

            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);

            return xlsxFilePath; 
        }

        /* -------------------- Right Click -------------------- */

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






