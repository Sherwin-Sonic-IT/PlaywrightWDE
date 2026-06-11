
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    public static class ManualPJPUploadEntryHelper
    {
        public static async Task<string> ExecuteManualPJPUploadEntryAsync(IPage page)
        {
            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                   ?? throw new Exception("❌ Report frame not found");

            await ManualPJPUploadEntryActions.SelectRadioButtonAsync(frame);
            await ManualPJPUploadEntryActions.ClickSubmitAsync(frame);
            return await ManualPJPUploadEntryActions.ClickDownloadAsync(page, frame);
        }
    }
}