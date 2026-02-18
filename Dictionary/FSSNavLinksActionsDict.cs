
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Navigation;

public static class FSSNavLinksActionsDict
{
    public static readonly Dictionary<string, string> Parents =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { FSSNavLinkSelectors.Parents["BIR"].Key, FSSNavLinkSelectors.Parents["BIR"].Display },
            { FSSNavLinkSelectors.Parents["MD"].Key,  FSSNavLinkSelectors.Parents["MD"].Display  },
            { FSSNavLinkSelectors.Parents["RP"].Key, FSSNavLinkSelectors.Parents["RP"].Display }
        };

    public static readonly Dictionary<string, Dictionary<string, string>> Children =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FSSNavLinkSelectors.Parents["BIR"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["BIR"]["Analytical"].Key, FSSNavLinkSelectors.Children["BIR"]["Analytical"].Display },
                    { FSSNavLinkSelectors.Children["BIR"]["PHReports"].Key,  FSSNavLinkSelectors.Children["BIR"]["PHReports"].Display  },
                    { FSSNavLinkSelectors.Children["BIR"]["PHPerfect"].Key,  FSSNavLinkSelectors.Children["BIR"]["PHPerfect"].Display  },
                    { FSSNavLinkSelectors.Children["BIR"]["CustomViews"].Key, FSSNavLinkSelectors.Children["BIR"]["CustomViews"].Display }
                }
            },
            {
                FSSNavLinkSelectors.Parents["MD"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["MD"]["General"].Key, FSSNavLinkSelectors.Children["MD"]["General"].Display }
                }
            },
            {
                FSSNavLinkSelectors.Parents["RP"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["RP"]["Sales"].Key, FSSNavLinkSelectors.Children["RP"]["Sales"].Display },
                    { FSSNavLinkSelectors.Children["RP"]["Master"].Key, FSSNavLinkSelectors.Children["RP"]["Master"].Display },
                    { FSSNavLinkSelectors.Children["RP"]["PromotionReports"].Key, FSSNavLinkSelectors.Children["RP"]["PromotionReports"].Display },
                    { FSSNavLinkSelectors.Children["RP"]["IntegratedPromotionsReports"].Key, FSSNavLinkSelectors.Children["RP"]["IntegratedPromotionsReports"].Display }
                }
            }
        };

    public static readonly Dictionary<string, Dictionary<string, NavNode[]>> Leaves =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FSSNavLinkSelectors.Parents["BIR"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FSSNavLinkSelectors.Children["BIR"]["Analytical"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical"]["DailySalesSummaryReport"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical"]["SalePeriodReport"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical"]["SalesContributionByStep"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical"]["SalesAnalysisActualVsTarget"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["BIR"]["PHReports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["PHReports"]["CaseFillReport"],
                            FSSNavLinkSelectors.Leaves["BIR"]["PHReports"]["ReachReport"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["BIR"]["PHPerfect"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["PHPerfect"]["FCSPlus"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["BIR"]["CustomViews"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["CustomViews"]["DTDashboard"]
                        }
                    }
                }
            },
            {
                FSSNavLinkSelectors.Parents["MD"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FSSNavLinkSelectors.Children["MD"]["General"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["MD"]["General"]["OutletMasterTracking"],
                            FSSNavLinkSelectors.Leaves["MD"]["General"]["SalesmanMasterTracking"]
                        }
                    }
                }
            },
            {
                FSSNavLinkSelectors.Parents["RP"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FSSNavLinkSelectors.Children["RP"]["Sales"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["Sales"]["SalesmanDailySalesReportRPT07"],
                            FSSNavLinkSelectors.Leaves["RP"]["Sales"]["InvoiceSummaryReportRPT08"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["RP"]["Master"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["SalesmanMasterReportRPT22"],
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["ArticleMasterReportRPT23"],
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["OutletMasterReportRPT24"],
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["OutletMasterStatusTrackReport"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["RP"]["PromotionReports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["PromotionReports"]["PromotionMasterReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["PromotionReports"]["PromotionsPromotionAllocationReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["PromotionReports"]["AuditTrailReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["PromotionReports"]["TradeDealMonitoringReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["PromotionReports"]["GisAuditReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["PromotionReports"]["OutletSubtypeThresholdReport"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["RP"]["IntegratedPromotionsReports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["IntegratedPromotionsReports"]["PromotionMasterReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["IntegratedPromotionsReports"]["PromotionSchemeWithSalesmanAndInvoice"],
                            FSSNavLinkSelectors.Leaves["RP"]["IntegratedPromotionsReports"]["SalesOrderVsPromotionsReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["IntegratedPromotionsReports"]["PromotionTrackingReport"],
                            FSSNavLinkSelectors.Leaves["RP"]["IntegratedPromotionsReports"]["IntegratedPromotionBudgetAllocationReport"]
                        }
                    }
                }
            }
        };
}
