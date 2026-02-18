
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;

namespace PlaywrightWDE.Global.Helpers
{
    public static class IFrameHelpers
    {
        private static async Task<IFrame> GetFrameAsync(
            IElementHandle iframeElement,
            string errorMessage)
        {
            var frame = await iframeElement.ContentFrameAsync();
            return frame ?? throw new PlaywrightException(errorMessage);
        }

        private static async Task<IFrame> GetMainDashboardFrameAsync(IPage page)
        {
            var iframeElement = await page.WaitForSelectorAsync(
                IFrameSelectors.IvuFrm_page0ivu4,
                new() { Timeout = 180_000 });

            if (iframeElement is null)
                throw new PlaywrightException("Main iframe element not found");

            return await GetFrameAsync(iframeElement, "Main iframe not loaded");
        }

        public static async Task<IFrame> GetDashboardSidebarIFrameAsync(IPage page)
        {
            return await GetMainDashboardFrameAsync(page);
        }

        public static async Task<IFrame> GetDashboardReportIFrameAsync(IPage page)
        {
            var mainFrame = await GetMainDashboardFrameAsync(page);

            var isolatedWorkArea = await mainFrame.WaitForSelectorAsync(
                IFrameSelectors.IsolatedWorkArea,
                new() { Timeout = 120_000 });

            if (isolatedWorkArea is null)
                throw new PlaywrightException("Isolated work area iframe not found");

            return await GetFrameAsync(
                isolatedWorkArea,
                "Isolated work area iframe not loaded");
        }
    }
}
