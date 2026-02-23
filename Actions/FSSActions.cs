
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;

namespace PlaywrightWDE.Actions
{
    public static class FSSActions
    {
        private static readonly string[] Sites = { "4049", "4A48", "4B48", "4C48", "4536", "4537" };

        private static readonly Dictionary<string, Func<IPage, string, Task>> RpMasterReports = new()
        {
            { "7.4.1", SalesmanMasterReportEntryHelper.ExecuteSalesmanMasterReportAsync },
            { "7.4.2", ArticleMasterReportEntryHelper.ExecuteArticleMasterReportAsync },
            { "7.4.3", OutletMasterReportEntryHelper.ExecuteOutletMasterReportAsync },
        };

        private static bool IsParent(string parentArg, string parentKey) =>
            parentArg.Equals(FSSNavLinkSelectors.Parents[parentKey].Key, StringComparison.OrdinalIgnoreCase);

        private static bool IsChild(string parentKey, string childArg, string childKey) =>
            childArg.Equals(FSSNavLinkSelectors.Children[parentKey][childKey].Key, StringComparison.OrdinalIgnoreCase);

        public static async Task ExecuteFssReportAsync(
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

            if (IsParent(parentArg, "RP") && IsChild("RP", childArg, "Master"))
            {
                if (!RpMasterReports.TryGetValue(leaf.Key, out var reportFunc))
                {
                    Logger.Log($"⚠️ Skipping unknown RP Master leaf: {leaf.Key} - {leaf.Display}");
                    return;
                }

                for (int i = 0; i < Sites.Length; i++)
                {
                    Logger.Log($"➡️ Executing report for site: {Sites[i]}");

                    await reportFunc(page, Sites[i]);

                    // await Task.Delay(500); 

                    if (i < Sites.Length - 1)
                        await ClickNavLinks.ClickNavLinksAsync(page, reportPath);
                }
            }
            else if (IsParent(parentArg, "BIR"))
            {
                if (IsChild("BIR", childArg, "Analytical"))
                    await DailySalesSumReportEntryHelper.ExecuteBIRDailySalesSumReportAsync(page);
                else
                    throw new Exception($"Unknown BIR child report: {childArg}");
            }
            else if (IsParent(parentArg, "MD"))
            {
                if (!IsChild("MD", childArg, "General"))
                    throw new Exception($"Unknown MD child report: {childArg}");
            }
            else
            {
                throw new Exception($"Unknown parent report: {parentArg}");
            }

            Logger.Log($"✅ Completed execution of report: {leaf.Display}");
        }
    }
}
