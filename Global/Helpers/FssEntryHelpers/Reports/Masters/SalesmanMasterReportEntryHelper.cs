
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    public static class SalesmanMasterReportEntryHelper
    {
        public static async Task<string> ExecuteSalesmanMasterReportAsync(IPage page, string siteCode)
        {
            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Salesman Master report frame not found");

            await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.SiteField.Selector, siteCode);

            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await CommonEntryActions.ClickChooseLayoutAsync(frame);
            await MasterReportsEntryActions.ClickFindAsync(frame);
            await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.SearchTermField.Selector, "/SSDI ALL");
            await MasterReportsEntryActions.SearchSelectDirectionValueAsync(frame);
            await MasterReportsEntryActions.ClickOkFindAsync(frame);
            await MasterReportsEntryActions.ClickCancelEscapeButtonAsync(frame);
            await MasterReportsEntryActions.ClickSsdiAllSalesmanAsync(frame);
            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame); 
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await MasterReportsEntryActions.EnterCustomFileNameAsync(frame, $"SALESMAN_MASTER_REPORT_{siteCode}");

            return await CommonEntryActions.ClickOkAsync(page, frame, CommonEntryHelpers.ReportType.SalesmanMaster, siteCode);
        }
    }
}




