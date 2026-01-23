using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    public static class SalesmanMasterReportHelper
    {
        public static async Task<string> ExecuteSalesmanMasterReportAsync(IPage page)
        {
            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelper.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Salesman Master report frame not found");

            await MasterReportEntry.EnterFieldAsync(
                frame,
                MasterReportSelector.MasterReportFields.SiteField.Selector,
                MasterReportSelector.MasterReportFields.SiteField.DefaultValue);

            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await CommonEntryActions.ClickChooseLayoutAsync(frame);
            await MasterReportEntry.ClickSSDIAllAsync(frame);

            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame); 
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await CommonEntryActions.EnterCustomFileNameAsync(frame, "SalesmanMasterReport");

            return await CommonEntryActions.ClickOkAsync(page, frame);
        }
    }
}



