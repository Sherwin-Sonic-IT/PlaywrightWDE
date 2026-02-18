using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Entry;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Helpers;


namespace PlaywrightWDE.Global.Helpers {

    public static class OutletMasterReportEntryHelper {

       public static async Task<string> ExecuteOutletMasterReportAsync(IPage page, string siteCode)
        {
            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Outlet Master report frame not found");

            await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.DeliverySite.Selector, siteCode);

            await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.StatusField.Selector,
                  MasterReportsEntrySelector.MasterReportFields.StatusField.DefaultValue
            );

            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await CommonEntryActions.ClickChooseLayoutAsync(frame);
            await MasterReportsEntryActions.ClickFindAsync(frame);
            await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.SearchTermField.Selector, "/SSDI_ALL");
            await MasterReportsEntryActions.SearchSelectDirectionValueAsync(frame);
            await MasterReportsEntryActions.ClickOkFindAsync(frame);
            await MasterReportsEntryActions.ClickCancelEscapeButtonAsync(frame);
            await MasterReportsEntryActions.ClickSsdiAllOutletAsync(frame);
            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame);
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await MasterReportsEntryActions.EnterCustomFileNameAsync(frame, $"OUTLETMASTER_REPORT_{siteCode}");

            return await CommonEntryActions.ClickOkAsync(page, frame, CommonEntryHelpers.ReportType.OutletMaster, siteCode);

        }
    }
}