// using Microsoft.Playwright;
// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Helpers;
// using PlaywrightWDE.Global.Selectors;

// namespace PlaywrightWDE.Global.Navigation
// {
//     public static class ClickNavLinks
//     {
//         public static async Task ClickNavLinksAsync(IPage page, string[] path)
//         {
//             var sidebarFrame = await IFrameHelper.GetDashboardSidebarIFrameAsync(page);

//             var treeContainer = sidebarFrame.Locator(FSSNavLinkSelectors.NavTree.DetailedNavigationTree);

//             async Task ExpandFolderAsync(string folderName)
//             {
//                 const int maxRetries = 10;

//                 for (int attempt = 1; attempt <= maxRetries; attempt++)
//                 {
//                     var nodeRow = treeContainer
//                         .Locator($"div:has(> a:text-is('{folderName}'))")
//                         .First;

//                     if (await nodeRow.CountAsync() > 0)
//                     {
//                         await nodeRow.ScrollIntoViewIfNeededAsync();

//                         var expandIcon = nodeRow.Locator(FSSNavLinkSelectors.NavTree.SItreeClosedFolder);
//                         if (await expandIcon.CountAsync() > 0)
//                         {
//                             await expandIcon.ClickAsync();
//                             await Task.Delay(500);
//                         }

//                         return;
//                     }

//                     await Task.Delay(500);
//                 }

//                 throw new PlaywrightException($"Folder '{folderName}' not found.");
//             }

//             foreach (var node in path.Take(path.Length - 1))
//             {
//                 await ExpandFolderAsync(node);
//             }

//             var finalNodeName = path.Last();
//             var reportLink = treeContainer
//                 .Locator($"a:text-is('{finalNodeName}')")
//                 .First;

//             await reportLink.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = 120_000
//             });

//             await reportLink.ScrollIntoViewIfNeededAsync();
//             await reportLink.ClickAsync();

//             Console.WriteLine($"✅ Clicked report '{finalNodeName}'");
//         }
//     }
// }







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
        // Default timeout in milliseconds (10 minutes)
        private const int DefaultTimeout = 600_000;

        public static async Task ClickNavLinksAsync(IPage page, string[] path)
        {
            var sidebarFrame = await IFrameHelper.GetDashboardSidebarIFrameAsync(page);
            var treeContainer = sidebarFrame.Locator(FSSNavLinkSelectors.NavTree.DetailedNavigationTree);

            async Task ExpandFolderAsync(string folderName)
            {
                var nodeRow = treeContainer.Locator($"div:has(> a:text-is('{folderName}'))").First;

                try
                {
                    // Wait until folder row is visible
                    await nodeRow.WaitForAsync(new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
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

                        await Task.Delay(500); // small pause after expanding
                    }
                }
                catch
                {
                    throw new PlaywrightException($"Folder '{folderName}' not found after waiting {DefaultTimeout / 1000} seconds.");
                }
            }

            // Expand all parent folders
            foreach (var node in path.Take(path.Length - 1))
            {
                await ExpandFolderAsync(node);
            }

            // Click the final report
            var finalNodeName = path.Last();
            var reportLink = treeContainer.Locator($"a:text-is('{finalNodeName}')").First;

            await reportLink.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
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
