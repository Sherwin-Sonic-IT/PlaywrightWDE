using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    public static class SalesOrderSummaryReportEntryHelper
    {
        public static async Task<string> ExecuteSalesOrderSummaryReportEntryAsync(IPage page, string siteCode)
        {
            await page.WaitForTimeoutAsync(15000);

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                   ?? throw new Exception("❌ Report frame not found");

            // await CommonEntryActions.EnterFieldAsync(frame, CommonEntrySelectors.CommonEntryFields.SiteField.Selector, siteCode);
            await CommonEntryActions.EnterFieldAsync(frame, CommonEntrySelectors.CommonEntryFields.DateField.Selector, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
            await CommonEntryActions.EnterFieldAsync(frame, CommonEntrySelectors.CommonEntryFields.SalesmanCodeFromField.Selector, "");
            await CommonEntryActions.SalesmanCodeFromF4Button(frame);
            await CommonEntryActions.SelectFirstAvailableCheckbox(frame); 
            await CommonEntryActions.ClickCopyButton(frame);
            await Task.Delay(800);
            await CommonEntryActions.EnterFieldAsync(frame, CommonEntrySelectors.CommonEntryFields.SalesmanCodeToField.Selector, "");
            await CommonEntryActions.SalesmanCodeToF4Button(frame);
            await CommonEntryActions.SelectLastAvailableCheckbox(frame); 
            await CommonEntryActions.ClickCopyButton(frame);
            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await CommonEntryActions.ClickChooseLayoutAsync(frame);
            await CommonEntryActions.ClickFindAsync(frame);
            await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.SearchTermField.Selector, "/INV_LQ_SSDI");
            await CommonEntryActions.SearchSelectDirectionValueAsync(frame);
            await CommonEntryActions.ClickOkFindAsync(frame);
            await CommonEntryActions.ClickCancelEscapeButtonAsync(frame);
            await CommonEntryActions.ClickInvLQSsdiAsync(frame);
            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame); 
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await CommonEntryActions.EnterCustomFileNameAsync(frame, $"SALES_ORDER_SUMMARY_REPORT_{siteCode}");

            return await CommonEntryActions.ClickOkAsync(page, frame, CommonEntryHelpers.ReportType.SalesOrderSummary, siteCode);        

        }
    }
}