using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Entry;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Helpers;


namespace PlaywrightWDE.Global.Helpers {

    public static class OutletMasterReportHelper {

       public static async Task<string> ExecuteOutletMasterReportAsync(IPage page)
        {
            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelper.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Outlet Master report frame not found");

            await MasterReportEntry.EnterFieldAsync(
                frame,
                MasterReportSelector.MasterReportFields.DeliverySite.Selector,
                MasterReportSelector.MasterReportFields.DeliverySite.DefaultValue
            );

            await MasterReportEntry.EnterFieldAsync(
                frame,
                MasterReportSelector.MasterReportFields.StatusField.Selector,
                MasterReportSelector.MasterReportFields.StatusField.DefaultValue
            );

            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await CommonEntryActions.ClickChooseLayoutAsync(frame);
            await MasterReportEntry.ClickSSDICAsync(frame);

            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame);
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await CommonEntryActions.EnterCustomFileNameAsync(frame, "OutletMasterReport");

            return await CommonEntryActions.ClickOkAsync(page, frame);

        }
    }
}

