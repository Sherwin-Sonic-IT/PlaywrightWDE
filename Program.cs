

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
            browser = await playwright.Chromium.LaunchAsync(new() { Headless = true }); // headless
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
                        var leafReportPath = new[]
                        {
                            FSSNavLinksActionsDict.Parents[parentArg],
                            FSSNavLinksActionsDict.Children[parentArg][childArg],
                            leaf.Display
                        };

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
            Console.ReadLine(); // holding the app open
        }
        catch (Exception ex)
        {
            Logger.Log("ERROR:");
            Logger.Log(ex.ToString());
        }
        finally
        {
            if (browser != null)
            {
                // int browserCloseDelay = 30;
                // Logger.Log($"⏳ Browser will close automatically after {browserCloseDelay} minutes...");
                // await Task.Delay(TimeSpan.FromMinutes(browserCloseDelay));
                // Logger.Log("🔒 Closing browser...");
                await browser.CloseAsync();
                // Logger.Log("✅ Browser closed successfully.");
            }
               
            playwright?.Dispose();
        }
    }

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
