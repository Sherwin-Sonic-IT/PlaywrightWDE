
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Navigations;
using PlaywrightWDE.Logs;
using PlaywrightWDE.Global.Helpers;

namespace PlaywrightWDE.Actions
{
    public static class HPCActions
    {
        private static readonly Dictionary<string, Func<IPage, Task>> PJPManualUpload = new()
        {
            { "4.3", ManualPJPUploadEntryHelper.ExecuteManualPJPUploadEntryAsync },
        };

        private static bool IsParent(string parentArg, string parentKey) =>
        parentArg.Equals(HPCNavLinkSelectors.Parents[parentKey].Key, StringComparison.OrdinalIgnoreCase);

        private static bool IsChild(string parentKey, string childArg, string childKey) =>
            childArg.Equals(HPCNavLinkSelectors.Children[parentKey][childKey].Key, StringComparison.OrdinalIgnoreCase);

        public static async Task ExecuteHpcActionAsync(
            IPage page,
            string parentArg,
            string childArg,
            NavNode? leaf,
            string[] reportPath)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (string.IsNullOrWhiteSpace(parentArg)) throw new ArgumentNullException(nameof(parentArg));
            if (string.IsNullOrWhiteSpace(childArg)) throw new ArgumentNullException(nameof(childArg));
            if (reportPath == null || reportPath.Length == 0) throw new ArgumentNullException(nameof(reportPath));

            await page.ClickAsync(HPCTopNavSelectors.TopNavButton.MasterData.Selector);

            await ClickNavLinks.ClickNavLinksAsync(page, reportPath);

            if(IsParent(parentArg, "PJP"))
            {
                if(IsChild("PJP", childArg, "Manual PJP Upload"))
                {
                  await ManualPJPUploadEntryHelper.ExecuteManualPJPUploadEntryAsync(page);
                }
            }

            Logger.Log(
                leaf != null
                    ? $"✅ Completed execution of report: {leaf.Display}"
                    : $"✅ Completed execution of child action: {childArg}"
            );
        }

    }
}
