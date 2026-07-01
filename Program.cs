
using Microsoft.Playwright;
using System;
using System.Linq;
using System.Threading.Tasks;
using PlaywrightWDE.Actions;
using PlaywrightWDE.Credentials;
using PlaywrightWDE.Logs;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Login;

class Program
{
    public static async Task Main(string[] args)
    {
        IPlaywright? playwright = null;
        IBrowser? browser = null;

        string? parentArg;
        string? childArg;
        NavNode[] selectedLeaves;

        try
        {
            playwright = await Playwright.CreateAsync();

            browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = true, // headless default is true, but set explicitly for clarity
                Args = new[]
                {
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--disable-gpu",
                    "--disable-dev-shm-usage"
                }
            });

            var page = await browser.NewPageAsync();

            Logger.Log("Task started.");

            if (args.Length < 3)
                throw new ArgumentException("Usage: dotnet run <FSS|FIGL|HPC> <PARENT> <CHILD> [REPORT]");

            var account = Accounts.Resolve(args);

            // ================= PARSE =================

            ParseReportArgs(
                args,
                account.Type,
                out parentArg,
                out childArg,
                out selectedLeaves
            );

            // ================= LOGIN =================
            var loginSuccess = await PerformLogin.PerformLoginAsync(
                page,
                account.Username,
                account.FirstPassword,
                account.SecondPassword
            );

            if (!loginSuccess)
                throw new Exception("Login failed");

            Logger.Log("Login successful.");

            // ================= EXECUTION =================
            switch (account.Type)
            {
                case AccountType.FSS:

                    if (parentArg == null || childArg == null)
                        throw new InvalidOperationException("Missing navigation args.");

                    var fssParent = FSSNavLinksActionsDict.Parents[parentArg];
                    var fssChild = FSSNavLinksActionsDict.Children[parentArg][childArg];

                    if (selectedLeaves.Length == 0)
                    {
                        Logger.Log($"No leaves found for {parentArg} -> {childArg}. Executing child only.");

                        var fssReportPath = new[]
                        {
                            fssParent,
                            fssChild
                        };

                        await FSSActions.ExecuteFssReportAsync(page, parentArg, childArg, null!, fssReportPath);
                    }
                    else
                    {
                        foreach (var leaf in selectedLeaves)
                        {
                            var fssReportPath = new[]
                            {
                                fssParent,
                                fssChild,
                                leaf.Display
                            };

                            await FSSActions.ExecuteFssReportAsync(page, parentArg, childArg, leaf, fssReportPath);
                        }
                    }

                    break;

                case AccountType.FIGL:
                    await HandleFigl(page, account, parentArg, childArg, selectedLeaves);
                    break;

                case AccountType.HPC:

                    if (parentArg == null || childArg == null)
                        throw new InvalidOperationException("Missing navigation args.");

                    var hpcParent = HPCNavLinksActionsDict.Parents[parentArg];
                    var hpcChild = HPCNavLinksActionsDict.Children[parentArg][childArg];

                    var hpcReportPath = new[]
                        {
                            hpcParent,
                            hpcChild
                        };


                    await HPCActions.ExecuteHpcActionAsync(page, parentArg,childArg, null!, hpcReportPath);

                    break;

                default:
                    throw new NotSupportedException();
            }

            Logger.Log("Task completed.");
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

    // ================= SAFE PARSER =================
    private static void ParseReportArgs(
    string[] args,
    AccountType accountType,
    out string parentArg,
    out string childArg,
    out NavNode[] selectedLeaves)
    {
        if (args.Length < 3)
            throw new ArgumentException("Usage: <ACCOUNT> <PARENT> <CHILD> [REPORT]");

        parentArg = args[1];
        childArg = args[2];

        string? reportArg = args.Length >= 4 ? args[3] : null;

        // HPC currently has no leaves
        if (accountType == AccountType.HPC)
        {
            selectedLeaves = Array.Empty<NavNode>();
            return;
        }

        // FSS leaves
        if (!FSSNavLinksActionsDict.Leaves.TryGetValue(parentArg, out var childDict) ||
            !childDict.TryGetValue(childArg, out var leaves))
        {
            selectedLeaves = Array.Empty<NavNode>();
            return;
        }

        if (!string.IsNullOrEmpty(reportArg))
        {
            selectedLeaves = leaves
                .Where(x => x.Key.Equals(reportArg, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }
        else
        {
            selectedLeaves = leaves;
        }
    }

    private static Task HandleFigl(
        IPage page,
        dynamic account,
        string parentArg,
        string childArg,
        NavNode[] selectedLeaves)
    {
        return Task.CompletedTask;
    }
}
