using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Navigation;

public static class FSSNavLinksActionsDict
{
    
    public static readonly Dictionary<string, string> Parents =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { FSSNavLinkSelectors.Parents.BIR.Key, FSSNavLinkSelectors.Parents.BIR.Display },
            { FSSNavLinkSelectors.Parents.MD.Key,  FSSNavLinkSelectors.Parents.MD.Display  },
            { FSSNavLinkSelectors.Parents.RP.Key, FSSNavLinkSelectors.Parents.RP.Display}
        };

    public static readonly Dictionary<string, Dictionary<string, string>> Children =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FSSNavLinkSelectors.Parents.BIR.Key,
                new Dictionary<string, string>
                {
                    { FSSNavLinkSelectors.Childrens.BIR.Analytical.Key, FSSNavLinkSelectors.Childrens.BIR.Analytical.Display },
                    { FSSNavLinkSelectors.Childrens.BIR.PHReports.Key,  FSSNavLinkSelectors.Childrens.BIR.PHReports.Display  },
                    { FSSNavLinkSelectors.Childrens.BIR.PHPerfect.Key,  FSSNavLinkSelectors.Childrens.BIR.PHPerfect.Display  },
                    { FSSNavLinkSelectors.Childrens.BIR.CustomViews.Key,FSSNavLinkSelectors.Childrens.BIR.CustomViews.Display}
                }
            },
            {
                FSSNavLinkSelectors.Parents.MD.Key,
                new Dictionary<string, string>
                {
                    { FSSNavLinkSelectors.Childrens.MD.General.Key, FSSNavLinkSelectors.Childrens.MD.General.Display }
                }
            },
            {
                FSSNavLinkSelectors.Parents.RP.Key,
                new Dictionary<string, string>
                {
                    { FSSNavLinkSelectors.Childrens.RP.Sales.Key, FSSNavLinkSelectors.Childrens.RP.Sales.Display },
                    { FSSNavLinkSelectors.Childrens.RP.Master.Key, FSSNavLinkSelectors.Childrens.RP.Master.Display },
                    { FSSNavLinkSelectors.Childrens.RP.PromotionReports.Key, FSSNavLinkSelectors.Childrens.RP.PromotionReports.Display },
                    { FSSNavLinkSelectors.Childrens.RP.IntegratedPromotionsReports.Key, FSSNavLinkSelectors.Childrens.RP.IntegratedPromotionsReports.Display }
                }
            }
        };

    public static readonly Dictionary<string, Dictionary<string, NavNode[]>> Leaves =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FSSNavLinkSelectors.Parents.BIR.Key,
                new Dictionary<string, NavNode[]>
                {
                    {
                        FSSNavLinkSelectors.Childrens.BIR.Analytical.Key,
                        new[] { FSSNavLinkSelectors.Leaves.BIR.Analytical.DailySalesSummaryReport, FSSNavLinkSelectors.Leaves.BIR.Analytical.SalePeriodReport, FSSNavLinkSelectors.Leaves.BIR.Analytical.SalesContributionByStep,
                        FSSNavLinkSelectors.Leaves.BIR.Analytical.SalesAnalysisActualVsTarget }
                    },
                    {
                        FSSNavLinkSelectors.Childrens.BIR.PHReports.Key,
                        new[] { FSSNavLinkSelectors.Leaves.BIR.PHReports.CaseFillReport, FSSNavLinkSelectors.Leaves.BIR.PHReports.ReachReport }
                    },
                    {
                        FSSNavLinkSelectors.Childrens.BIR.PHPerfect.Key,
                        new[] { FSSNavLinkSelectors.Leaves.BIR.PHPerfect.FCSPlus }
                    },
                    {
                        FSSNavLinkSelectors.Childrens.BIR.CustomViews.Key,
                        new[] { FSSNavLinkSelectors.Leaves.BIR.CustomViews.DTDashboard }
                    }
                }
            },
            {
                FSSNavLinkSelectors.Parents.MD.Key,
                new Dictionary<string, NavNode[]>
                {
                    {
                        FSSNavLinkSelectors.Childrens.MD.General.Key,
                        new[] { FSSNavLinkSelectors.Leaves.MD.General.OutletMasterTracking, FSSNavLinkSelectors.Leaves.MD.General.SalesmanMasterTracking }
                    }
                }
            },
            {
                FSSNavLinkSelectors.Parents.RP.Key,
                new Dictionary<string, NavNode[]>
                {
                    {
                        FSSNavLinkSelectors.Childrens.RP.Sales.Key,
                        new[] { FSSNavLinkSelectors.Leaves.RP.Sales.SalesmanDailySalesReportRPT07, FSSNavLinkSelectors.Leaves.RP.Sales.InvoiceSummaryReportRPT08 }
                    },
                     {
                        // NavLinkSelector.Childrens.RP.Master.Key,
                        // new[] { NavLinkSelector.Leaves.RP.Master.SalesmanMasterReportRPT22, NavLinkSelector.Leaves.RP.Master.ArticleMasterReportRPT23, NavLinkSelector.Leaves.RP.Master.OutletMasterReportRPT24 }
                        
                        FSSNavLinkSelectors.Childrens.RP.Master.Key,
                        new[] { FSSNavLinkSelectors.Leaves.RP.Master.SalesmanMasterReportRPT22, FSSNavLinkSelectors.Leaves.RP.Master.ArticleMasterReportRPT23, FSSNavLinkSelectors.Leaves.RP.Master.OutletMasterReportRPT24,
                        FSSNavLinkSelectors.Leaves.RP.Master.OutletMasterStatusTrackReport }
                    },
                    {
                        FSSNavLinkSelectors.Childrens.RP.PromotionReports.Key,
                        new[] { FSSNavLinkSelectors.Leaves.RP.PromotionReports.PromotionMasterReport, FSSNavLinkSelectors.Leaves.RP.PromotionReports.PromotionsPromotionAllocationReport, FSSNavLinkSelectors.Leaves.RP.PromotionReports.AuditTrailReport,
                        FSSNavLinkSelectors.Leaves.RP.PromotionReports.TradeDealMonitoringReport, FSSNavLinkSelectors.Leaves.RP.PromotionReports.GisAuditReport, FSSNavLinkSelectors.Leaves.RP.PromotionReports.OutletSubtypeThresholdReport}
                    },
                    {
                        FSSNavLinkSelectors.Childrens.RP.IntegratedPromotionsReports.Key,
                        new [] { FSSNavLinkSelectors.Leaves.RP.IntegratedPromotionsReport.PromotionMasterReport, FSSNavLinkSelectors.Leaves.RP.IntegratedPromotionsReport.PromotionSchemeWithSalesmanAndInvoice, FSSNavLinkSelectors.Leaves.RP.IntegratedPromotionsReport.SalesOrderVsPromotionsReport,
                        FSSNavLinkSelectors.Leaves.RP.IntegratedPromotionsReport.PromotionTrackingReport, FSSNavLinkSelectors.Leaves.RP.IntegratedPromotionsReport.IntegratedPromotionBudgetAllocationReport}
                    }
                }
            }
        };
        
}



