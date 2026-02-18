
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Entry;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Helpers;

namespace PlaywrightWDE.Global.Helpers {

    public static class ArticleMasterReportEntryHelper {

        public static async Task<string> ExecuteArticleMasterReportAsync(IPage page, string siteCode)
        {

            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Article Master report frame not found");

                await CommonEntryActions.EnterFieldAsync(frame,MasterReportsEntrySelector.MasterReportFields.SiteField.Selector, siteCode);

                await MasterReportsEntryActions.SelectAllRadioButtonAsync(frame);
                await Task.Delay(5000);
                await CommonEntryActions.ClickExecuteAsync(frame);
                await CommonEntryActions.ClickMoreAsync(frame);
                await CommonEntryActions.ClickChooseLayoutAsync(frame);
                await MasterReportsEntryActions.ClickFindAsync(frame);
                await CommonEntryActions.EnterFieldAsync(frame, MasterReportsEntrySelector.MasterReportFields.SearchTermField.Selector, "/SSDI_MSTR");
                await MasterReportsEntryActions.SearchSelectDirectionValueAsync(frame);
                await MasterReportsEntryActions.ClickOkFindAsync(frame);
                await MasterReportsEntryActions.ClickCancelEscapeButtonAsync(frame);
                await MasterReportsEntryActions.ClickSsdiMstrArticleAsync(frame);
                await CommonEntryActions.ClickMenuAsync(frame);
                await CommonEntryActions.HoverListAsync(frame);
                await CommonEntryActions.HoverExportAsync(frame);
                await CommonEntryActions.ClickSpreadsheetAsync(frame);

                await MasterReportsEntryActions.EnterCustomFileNameAsync(frame, $"ARTICLEMASTER_REPORT_{siteCode}");

                return await CommonEntryActions.ClickOkAsync(page, frame, CommonEntryHelpers.ReportType.ArticleMaster, siteCode);
        }
    }
}




