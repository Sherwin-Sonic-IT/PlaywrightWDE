using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;
using PlaywrightWDE.Global.Logs;


namespace PlaywrightWDE.Global.Helpers
{
    public static class ChangeSiteEntryHelper
    {
        public static async Task<string> ExecuteChangeSiteEntryAsync(IPage page, string siteCode)
        {
            await page.WaitForTimeoutAsync(10000);

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                   ?? throw new Exception("❌ Report frame not found");

            await CommonEntryActions.EnterFieldAsync(frame, ChangeSiteEntrySelector.ChangeSiteRepFields.SiteField.Selector, siteCode);

            return await ChangeSiteEntryActions.SaveAsync(frame);
        }
    }
}