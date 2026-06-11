using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Logs;

namespace PlaywrightWDE.Global.Entry {


    public static class CommonEntryActions {

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
                Logger.Log("⚠️ Calendar parse failed, using today");
            }

            var exportFolder = FilePath.FilePath.GetDatedExportFolder(extractedDate);
            var stagingFolder = Path.Combine(exportFolder, "staging");
            Directory.CreateDirectory(stagingFolder);

            var download = await page.RunAndWaitForDownloadAsync(
                () => CommonEntryHelpers.ClickAsync(
                    frame,
                    MasterReportsEntrySelector.MasterReportButtons.OkButton.Selector,
                    "✅ Clicked OK"),
                new() { Timeout = CommonEntryHelpers.DefaultTimeout });

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

        public static async Task SelectAllRadioButtonAsync(IFrame frame)
        {
             await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportRadioButtons.AllRadioButton.Selector,
                "✅ All radio button clicked");
        }

        public static async Task ClickFindAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.FindButton.Selector,
                "✅ Clicked find");
        }

        public static async Task SearchSelectDirectionValueAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.SearchDirectionDropdownButton.Selector,
                "✅ Clicked search direction dropdown"); 

            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportEntryMenuItems.SearchDirectionItem.Selector,
                "✅ Selected search direction item");
        }

        public static async Task ClickOkFindAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.OkFindButton.Selector,
                "✅ Clicked ok find button"
            );

            await Task.Delay(5000);
        }

        public static async Task ClickCancelEscapeButtonAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.CancelEscapeButton.Selector,
                "✅ Clicked cancel (escape) button"
            );
        }

        public static async Task ClickSsdiAllSalesmanAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportFields.SsdiAllSalesmanField.Selector,
                "✅ Clicked /SSDI_ALL");
        }

        public static async Task ClickSsdiAllOutletAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportFields.SsdiAllOutletField.Selector,
                "✅ Clicked /SSDI_ALL");
        }
            
        public static async Task ClickSsdiMstrArticleAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportFields.SsdiMstrArticleField.Selector,
                "✅ Clicked /SSDI MSTR");
        }

        public static async Task ClickInvLQSsdiAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                SalesmanDailySalesReportEntrySelector.SalesmanDailySalesReportRepFields.InvLQSsdiField.Selector,
                "✅ Clicked /INV_LQ_SSDI");
        }

        public static async Task EnterCustomFileNameAsync(
            IFrame frame,
            string fileName)
        {
            await CommonEntryHelpers.FillAsync(
                frame,
                MasterReportsEntrySelector.MasterReportFields.FileNameField.Selector,
                fileName,
                $"✅ File name changed to '{fileName}'");
        }


        public static async Task<string> SalesmanCodeFromF4Button (IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.SalesmanCodeFromF4Button.Selector,
                "✅ Clicked Salesman Button From List");

            return "Clicked";
        }

       public static async Task<string> SalesmanCodeToF4Button (IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
               CommonEntrySelectors.CommonEntryButtons.SalesmanCodeToF4Button.Selector,
                "✅ Clicked Salesman Button To List");

            return "Clicked";
        }

        public static async Task<string> SelectFirstAvailableCheckbox(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.FirstRowCheckbox.Selector,
                "✅ Selected first available checkbox");

            return "Clicked";
        }

        public static async Task<string> SelectLastAvailableCheckbox(IFrame frame)
        {
            await CommonEntryHelpers.ScrollSapF4ToBottomAsync(frame, CommonEntrySelectors.CommonEntryButtons.ScrollSap4ToBottom.Selector);

            await CommonEntryHelpers.ClickAsync(
                frame,
                CommonEntrySelectors.CommonEntryButtons.LastRowCheckbox.Selector,
                "✅ Selected last available checkbox");

            return "Clicked";
        }

        public static async Task<string> ClickCopyButton(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
               CommonEntrySelectors.CommonEntryButtons.CopyButton.Selector,
                "✅ Clicked Copy button");

            return "Clicked";
        }

    }
    
}



