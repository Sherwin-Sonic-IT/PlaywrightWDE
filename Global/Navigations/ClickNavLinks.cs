using Microsoft.Playwright;
using System;
using System.Linq;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Selectors;

namespace PlaywrightWDE.Global.Navigation
{
    public static class ClickNavLinks
    {
        private const int DefaultTimeout = 600_000;

        public static async Task ClickNavLinksAsync(IPage page, string[] path)
        {
            var sidebarFrame = await IFrameHelpers.GetDashboardSidebarIFrameAsync(page);
            var treeContainer = sidebarFrame.Locator(FSSNavLinkSelectors.NavTree.DetailedNavigationTree);

            async Task ExpandFolderAsync(string folderName)
            {
                var nodeRow = treeContainer.Locator($"div:has(> a:text-is('{folderName}'))").First;

                try
                {
                    await nodeRow.WaitForAsync(new LocatorWaitForOptions
                    {
                        // State = WaitForSelectorState.Visible,
                        Timeout = DefaultTimeout
                    });

                    await nodeRow.ScrollIntoViewIfNeededAsync();

                    var expandIcon = nodeRow.Locator(FSSNavLinkSelectors.NavTree.SItreeClosedFolder);
                    if (await expandIcon.CountAsync() > 0)
                    {
                        await expandIcon.ClickAsync(new LocatorClickOptions
                        {
                            Timeout = DefaultTimeout
                        });

                        // await Task.Delay(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new PlaywrightException($"Folder '{folderName}' not found after waiting {DefaultTimeout} minutes.", ex);
                }
            }

            foreach (var node in path.Take(path.Length - 1))
            {
                await ExpandFolderAsync(node);
            }

            var finalNodeName = path.Last();
            var reportLink = treeContainer.Locator($"a:text-is('{finalNodeName}')").First;

            await reportLink.WaitForAsync(new LocatorWaitForOptions
            {
                // State = WaitForSelectorState.Visible,
                Timeout = DefaultTimeout
            });

            await reportLink.ScrollIntoViewIfNeededAsync();

            await reportLink.ClickAsync(new LocatorClickOptions
            {
                Timeout = DefaultTimeout
            });

            Console.WriteLine($"✅ Clicked report '{finalNodeName}'");
        }
    }
}
