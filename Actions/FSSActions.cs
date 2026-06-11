
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Logs;
using PlaywrightWDE.Navigations;

namespace PlaywrightWDE.Actions
{
    public static class FSSActions
    {

        private static readonly Dictionary<string, Func<IPage, string, Task>> RpMasterReports = new()
        {
            { "7.4.1", SalesmanMasterReportEntryHelper.ExecuteSalesmanMasterReportAsync },
            { "7.4.2", ArticleMasterReportEntryHelper.ExecuteArticleMasterReportAsync },
            { "7.4.3", OutletMasterReportEntryHelper.ExecuteOutletMasterReportAsync },
        };

        private static readonly Dictionary<string, Func<IPage, Task>> BirReports = new()
        {
            { "1.1.1", DailySalesSumReportEntryHelper.ExecuteBIRDailySalesSumReportAsync },
        };

        private static readonly Dictionary<string, Func<IPage, string, Task>> AdminChangeSite = new()
        {
            { "10.1", ChangeSiteEntryHelper.ExecuteChangeSiteEntryAsync },
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

            if (IsParent(parentArg, "RP"))
            {
                if(IsChild("RP", childArg, "Master"))
                {
                    if (!RpMasterReports.TryGetValue(leaf.Key, out var reportFunc))
                    {
                        Logger.Log($"⚠️ Skipping unknown RP Master leaf: {leaf.Key} - {leaf.Display}");
                        return;
                    }

                    for (int i = 0; i < CommonEntryHelpers.Sites.Length; i++)
                    {
                        Logger.Log($"➡️ Executing report for site: {CommonEntryHelpers.Sites[i]}");

                        await reportFunc(page, CommonEntryHelpers.Sites[i]);

                        if (i < CommonEntryHelpers.Sites.Length - 1)
                            await ClickNavLinks.ClickNavLinksAsync(page, reportPath);
                    }
                }
                
            }
            else if (IsParent(parentArg, "BIR"))
            {
                if (IsChild("BIR", childArg, "Analytical Reports"))
                {
                    if (!BirReports.TryGetValue(leaf.Key, out var reportFunc))
                    {
                        Logger.Log($"⚠️ Skipping unknown BIR Analytical Reports leaf: {leaf.Key} - {leaf.Display}");
                        return;
                    }

                    await reportFunc(page);
                }
                else
                {
                    throw new Exception($"Unknown BIR child report: {childArg}");
                }
            }
            else if (IsParent(parentArg, "AM"))
            {
                if (IsChild("AM", childArg, "10.1 Change Site"))
                {
                    if (!AdminChangeSite.TryGetValue("10.1", out var reportFunc))
                    {
                        throw new Exception("Unknown AM entry: 10.1");
                    }

                    for (int i = 0; i < CommonEntryHelpers.Sites.Length; i++)
                    {
                        Logger.Log($"➡️ Executing Change Site for: {CommonEntryHelpers.Sites[i]}");

                        await reportFunc(page, CommonEntryHelpers.Sites[i]);

                        await ClickNavLinks.ClickNavLinksAsync(page, new[]
                        {
                            FSSNavLinksActionsDict.Parents["RP"],
                            FSSNavLinksActionsDict.Children["RP"]["S"],
                            "7.2.2 Invoice Summary Report (RPT08)"
                        });

                        Logger.Log("✅ Navigated to Invoice Summary Report");

                        await InvoiceSummaryReportEntryHelper.ExecuteInvoiceSummaryReportEntryAsync(page, CommonEntryHelpers.Sites[i]);

                        Logger.Log("✅ Navigated to Sales Order Summary Report");

                        await ClickNavLinks.ClickNavLinksAsync(page, new[]
                        {
                            FSSNavLinksActionsDict.Parents["RP"],
                            FSSNavLinksActionsDict.Children["RP"]["S"],
                            "7.2.6 Sales Order Summary Report (RPT12)"
                        });

                        await SalesOrderSummaryReportEntryHelper.ExecuteSalesOrderSummaryReportEntryAsync(page, CommonEntryHelpers.Sites[i]);

                        if (i < CommonEntryHelpers.Sites.Length - 1)
                            await ClickNavLinks.ClickNavLinksAsync(page, reportPath);
                    }
                }
                else    
                {
                    throw new Exception($"Unknown AM child report: {childArg}");
                }
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

            Logger.Log(
                leaf != null
                    ? $"✅ Completed execution of report: {leaf.Display}"
                    : $"✅ Completed execution of child action: {childArg}"
            );

        }
    }
}
