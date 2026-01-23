// using Microsoft.Playwright;
// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using PlaywrightWDE.Actions;
// using PlaywrightWDE.Credentials;
// using PlaywrightWDE.Global.Logs;
// using PlaywrightWDE.Global.Navigation;
// using PlaywrightWDE.Global.Selectors;
// using PlaywrightWDE.Login;

// class Program
// {
//     public static async Task Main(string[] args)
//     {
//         IPlaywright? playwright = null;
//         IBrowser? browser = null;

//         string? parentArg = null;
//         string? childArg = null;
//         // string? reportArg = null;
//         string[]? reportPath = null;
//         NavNode? selectedLeaf = null;

//         try
//         {
//             playwright = await Playwright.CreateAsync();
//             browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
//             var page = await browser.NewPageAsync();

//             Logger.Log("Task started.");

//             if (args.Length == 0)
//                 throw new ArgumentException("Usage: dotnet run <FSS|FIGL|HPC> [...]");

//             var account = Accounts.Resolve(args);

//             // ---------- ARG PARSING FOR FSS/FIGL ----------
//             if (account.Type == AccountType.FSS)
//             {
//                 ParseReportArgs(
//                     args,
//                     parentKey => FSSNavLinksActionsDict.Parents[parentKey],
//                     (parentKey, childKey) => FSSNavLinksActionsDict.Children[parentKey][childKey],
//                     (parentKey, childKey) => FSSNavLinksActionsDict.Leaves[parentKey][childKey],
//                     out parentArg, out childArg, out reportPath, out selectedLeaf
//                 );
//             }
//             else if (account.Type == AccountType.FIGL)
//             {
//                 ParseReportArgs(
//                 args,
//                 parentKey => FIGLNavLinksActionDict.Parents[parentKey],
//                 (parentKey, childKey) => FIGLNavLinksActionDict.Children[parentKey][childKey],
//                 (parentKey, childKey) => FIGLNavLinksActionDict.Leaves[parentKey][childKey],
//                 out parentArg, out childArg, out reportPath, out selectedLeaf
//                 );
//             }

//             // ---------- LOGIN ----------
//            var loginSuccess = await PerformLogin.PerformLoginAsync(
//                 page,
//                 account.Username,
//                 account.FirstPassword,
//                 account.SecondPassword
//             );


//             if (!loginSuccess)
//                 throw new Exception("Login failed");

//             Logger.Log("Login successful.");

//             // ---------- EXECUTION ----------
//             switch (account.Type)
//             {
//                 case AccountType.FSS:
//                     if (parentArg == null || childArg == null || reportPath == null || selectedLeaf == null)
//                         throw new InvalidOperationException("FSS arguments were not initialized.");
//                     await FSSActions.ExecuteFssReportAsync(page, parentArg, childArg, selectedLeaf, reportPath);
//                     break;

//                 case AccountType.FIGL:
//                     if (parentArg == null || childArg == null || reportPath == null || selectedLeaf == null)
//                         throw new InvalidOperationException("FIGL arguments were not initialized.");
//                     await FIGLActions.ExecuteFiglReportAsync(page, parentArg, childArg, selectedLeaf, reportPath);
//                     break;

//                 case AccountType.HPC:
//                     await HPCActions.ExecuteHpcActionsAsync(page);
//                     break;

//                 default:
//                     throw new NotSupportedException($"Unsupported account type: {account.Type}");
//             }

//             Logger.Log("Task completed.");
//             Console.ReadLine();
//         }
//         catch (Exception ex)
//         {
//             Logger.Log("ERROR:");
//             Logger.Log(ex.ToString());
//         }
//         finally
//         {
//             if (browser != null)
//                 await browser.CloseAsync();

//             playwright?.Dispose();
//         }
//     }

//     /// <summary>
//     /// Dynamically parses report arguments for FSS/FIGL accounts.
//     /// </summary>
   
//    private static void ParseReportArgs(
//     string[] args,
//     Func<string, string> getParent,
//     Func<string, string, string> getChild,
//     Func<string, string, NavNode[]> getLeaves,
//     out string parentArg,
//     out string childArg,
//     out string[] reportPath,
//     out NavNode selectedLeaf)
//     {
//         if (args.Length < 3)
//             throw new ArgumentException("Insufficient arguments. Usage: <ACCOUNT> <PARENT> <CHILD> [REPORT]");

//         parentArg = args[1].ToUpper();
//         childArg = args[2].ToUpper();
//         string? reportArg = args.Length >= 4 ? args[3].ToUpper() : null; 

//         var parent = getParent(parentArg);
//         var child = getChild(parentArg, childArg);
//         var leaves = getLeaves(parentArg, childArg);

//         string[] leavesStrings;

//         if (!string.IsNullOrEmpty(reportArg))
//         {
//             selectedLeaf = leaves.FirstOrDefault(n => n.Key.ToUpper() == reportArg)
//                 ?? throw new ArgumentException($"Report '{reportArg}' not found under {parentArg} -> {childArg}");
//             leavesStrings = new[] { selectedLeaf.Display };
//         }
//         else
//         {
//             selectedLeaf = leaves.First();
//             leavesStrings = leaves.Select(n => n.Display).ToArray();
//         }

//         reportPath = new[] { parent, child }.Concat(leavesStrings).ToArray();
//         Logger.Log($"Report path: {string.Join(" -> ", reportPath)}");
//     }

// }











using Microsoft.Playwright;
using System;
using System.Linq;
using System.Threading.Tasks;
using PlaywrightWDE.Actions;
using PlaywrightWDE.Credentials;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Login;

class Program
{
    public static async Task Main(string[] args)
    {
        IPlaywright? playwright = null;
        IBrowser? browser = null;

        string? parentArg = null;
        string? childArg = null;
        NavNode[]? selectedLeaves = null;

        try
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browser.NewPageAsync();

            Logger.Log("Task started.");

            if (args.Length == 0)
                throw new ArgumentException("Usage: dotnet run <FSS|FIGL|HPC> [...]");

            var account = Accounts.Resolve(args);

            // ---------- ARG PARSING FOR FSS/FIGL ----------
            if (account.Type == AccountType.FSS)
            {
                ParseReportArgs(
                    args,
                    parentKey => FSSNavLinksActionsDict.Parents[parentKey],
                    (parentKey, childKey) => FSSNavLinksActionsDict.Children[parentKey][childKey],
                    (parentKey, childKey) => FSSNavLinksActionsDict.Leaves[parentKey][childKey],
                    out parentArg, out childArg, out selectedLeaves
                );
            }
            else if (account.Type == AccountType.FIGL)
            {
                ParseReportArgs(
                    args,
                    parentKey => FIGLNavLinksActionDict.Parents[parentKey],
                    (parentKey, childKey) => FIGLNavLinksActionDict.Children[parentKey][childKey],
                    (parentKey, childKey) => FIGLNavLinksActionDict.Leaves[parentKey][childKey],
                    out parentArg, out childArg, out selectedLeaves
                );
            }

            // ---------- LOGIN ----------
            var loginSuccess = await PerformLogin.PerformLoginAsync(
                page,
                account.Username,
                account.FirstPassword,
                account.SecondPassword
            );

            if (!loginSuccess)
                throw new Exception("Login failed");

            Logger.Log("Login successful.");

            // ---------- EXECUTION ----------
            switch (account.Type)
            {
                case AccountType.FSS:
                    if (parentArg == null || childArg == null || selectedLeaves == null)
                        throw new InvalidOperationException("FSS arguments were not initialized.");

                    foreach (var leaf in selectedLeaves)
                    {
                        // Build the leaf navigation path
                        var leafReportPath = new[]
                        {
                            FSSNavLinksActionsDict.Parents[parentArg],
                            FSSNavLinksActionsDict.Children[parentArg][childArg],
                            leaf.Display
                        };

                        // Execute report (handles fresh frame internally)
                        await FSSActions.ExecuteFssReportAsync(page, parentArg, childArg, leaf, leafReportPath);
                    }
                    break;

                case AccountType.FIGL:
                    if (parentArg == null || childArg == null || selectedLeaves == null)
                        throw new InvalidOperationException("FIGL arguments were not initialized.");

                    foreach (var leaf in selectedLeaves)
                    {
                        var leafReportPath = new[]
                        {
                            FIGLNavLinksActionDict.Parents[parentArg],
                            FIGLNavLinksActionDict.Children[parentArg][childArg],
                            leaf.Display
                        };

                        Logger.Log($"Executing report: {string.Join(" -> ", leafReportPath)}");

                        await ClickNavLinks.ClickNavLinksAsync(page, leafReportPath);
                        await FIGLActions.ExecuteFiglReportAsync(page, parentArg, childArg, leaf, leafReportPath);
                    }
                    break;

                case AccountType.HPC:
                    await HPCActions.ExecuteHpcActionsAsync(page);
                    break;

                default:
                    throw new NotSupportedException($"Unsupported account type: {account.Type}");
            }

            Logger.Log("Task completed.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Logger.Log("ERROR:");
            Logger.Log(ex.ToString());
        }
        finally
        {
            if (browser != null)
                await browser.CloseAsync();

            playwright?.Dispose();
        }
    }

    /// <summary>
    /// Dynamically parses report arguments for FSS/FIGL accounts.
    /// </summary>
    private static void ParseReportArgs(
        string[] args,
        Func<string, string> getParent,
        Func<string, string, string> getChild,
        Func<string, string, NavNode[]> getLeaves,
        out string parentArg,
        out string childArg,
        out NavNode[] selectedLeaves)
    {
        if (args.Length < 3)
            throw new ArgumentException("Insufficient arguments. Usage: <ACCOUNT> <PARENT> <CHILD> [REPORT]");

        parentArg = args[1].ToUpper();
        childArg = args[2].ToUpper();
        string? reportArg = args.Length >= 4 ? args[3] : null;

        var leaves = getLeaves(parentArg, childArg);

        if (!string.IsNullOrEmpty(reportArg))
        {
            selectedLeaves = leaves
                .Where(n => n.Key.Equals(reportArg, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (selectedLeaves.Length == 0)
                throw new ArgumentException($"Report '{reportArg}' not found under {parentArg} -> {childArg}");
        }
        else
        {
            selectedLeaves = leaves;
        }
    }
}
