
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Logs;

namespace PlaywrightWDE.Actions
{
    public static class FIGLActions
    {
        public static async Task ExecuteFiglReportAsync(
            IPage page,
            string parentArg,
            string childArg,
            NavNode leaf,
            string[] reportPath)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (string.IsNullOrWhiteSpace(parentArg)) throw new ArgumentNullException(nameof(parentArg));
            if (string.IsNullOrWhiteSpace(childArg)) throw new ArgumentNullException(nameof(childArg));
            if (reportPath == null || reportPath.Length == 0) throw new ArgumentNullException(nameof(reportPath));

            await ClickNavLinks.ClickNavLinksAsync(page, reportPath);

            if (parentArg.Equals(FIGLNavLinkSelectors.Parents["RP"].Key, StringComparison.OrdinalIgnoreCase) &&
                childArg.Equals(FIGLNavLinkSelectors.Children["RP"]["Procurement"].Key, StringComparison.OrdinalIgnoreCase))
            {
                if (leaf.Key.Equals(FIGLNavLinkSelectors.Leaves["RP"]["Procurement"]["GoodReceiptsReport"].Key, StringComparison.OrdinalIgnoreCase))
                {
                    await GoodReceiptsReportEntryHelper.ExecuteGoodReceiptsReportAsync(page);
                }
                else
                {
                    Logger.Log($"⚠️ Skipping unknown FIGL leaf: {leaf.Key} - {leaf.Display}");
                    return;
                }
            }
            else
            {
                throw new Exception($"Unknown FIGL parent/child combination: {parentArg}/{childArg}");
            }

            Logger.Log($"✅ Completed execution of report: {leaf.Display}");
        }
    }
}
