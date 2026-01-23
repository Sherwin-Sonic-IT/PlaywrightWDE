using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Entry;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Helpers;

namespace PlaywrightWDE.Global.Helpers {

    public static class ArticleMasterReportHelper {

        public static async Task<string> ExecuteArticleMasterReportAsync(IPage page)
        {

            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelper.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Article Master report frame not found");

            await MasterReportEntry.EnterFieldAsync(
                frame,
                MasterReportSelector.MasterReportFields.SiteField.Selector,
                MasterReportSelector.MasterReportFields.SiteField.DefaultValue);

                await MasterReportEntry.SelectAllRadioButtonAsync(frame);

                await CommonEntryActions.ClickExecuteAsync(frame);
                await CommonEntryActions.ClickMoreAsync(frame);
                await CommonEntryActions.ClickChooseLayoutAsync(frame);
                await MasterReportEntry.ClickSSDIMSTRAsync(frame);

                await CommonEntryActions.ClickMenuAsync(frame);
                await CommonEntryActions.HoverListAsync(frame);
                await CommonEntryActions.HoverExportAsync(frame);
                await CommonEntryActions.ClickSpreadsheetAsync(frame);

                await CommonEntryActions.EnterCustomFileNameAsync(frame, "ArticleMasterReport");

                return await CommonEntryActions.ClickOkAsync(page, frame);
        }
    }
}




