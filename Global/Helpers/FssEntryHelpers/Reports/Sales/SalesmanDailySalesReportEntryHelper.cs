using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    
    public static class SalesmanDailySalesReportEntryHelper
    {
        public static async Task<string> ExecuteSalesmanDailySalesReportEntryAsync(IPage page, string siteCode)
        {
            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                   ?? throw new Exception("❌ Report frame not found");

            // await CommonEntryActions.EnterFieldAsync(frame, SalesmanDailySalesReportEntrySelector.SalesmanDailySalesReportRepFields.SiteField.Selector, siteCode);
            await CommonEntryActions.EnterFieldAsync(frame, SalesmanDailySalesReportEntrySelector.SalesmanDailySalesReportRepFields.InvoiceDateField.Selector,
                  SalesmanDailySalesReportEntrySelector.SalesmanDailySalesReportRepFields.InvoiceDateField.DefaultValue);
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

            await CommonEntryActions.EnterCustomFileNameAsync(frame, $"SALESMAN_DAILY_SALES_REPORT_{siteCode}");

            return await CommonEntryActions.ClickOkAsync(page, frame, CommonEntryHelpers.ReportType.SalesDailySales, siteCode);
        }
    }
}