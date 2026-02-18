using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Navigation;

namespace PlaywrightWDE.Global.Selectors
{
    public static class FSSNavLinkSelectors
    {
        public record ChangeDrillDown(string Selector, string DisplayName);
        public record FieldEntry(string Selector, string DefaultValue);

        public static class NavTree
        {
            public const string DetailedNavigationTree = "#DetailedNavigationTree";
            public const string SItreeClosedFolder = "img.SItreeClosedFolder";
        }

        public static readonly Dictionary<string, NavNode> Parents = new()
        {
            ["BIR"] = new NavNode("BIR", "Business Information Reports"),
            ["MD"] = new NavNode("MD", "Master Data"),
            ["RP"] = new NavNode("RP", "Reports")
        };

        public static readonly Dictionary<string, Dictionary<string, NavNode>> Children = new()
        {
            ["BIR"] = new()
            {
                ["Analytical"] = new NavNode("AR", "1 Analytical Reports"),
                ["PHReports"] = new NavNode("PR", "4.1 PH Reports"),
                ["PHPerfect"] = new NavNode("PPR", "5.1 PH Perfect Reports"),
                ["CustomViews"] = new NavNode("CV", "Custom Views")
            },
            ["MD"] = new()
            {
                ["General"] = new NavNode("G", "4.5 General")
            },
            ["RP"] = new()
            {
                ["Sales"] = new NavNode("S", "7.2 Sales"),
                ["Master"] = new NavNode("M", "7.4 Master"),
                ["PromotionReports"] = new NavNode("PR", "7.8 Promotion Reports"),
                ["IntegratedPromotionsReports"] = new NavNode("IPR", "7.9 Integrated Promotions reports")
            }
        };

        public static readonly Dictionary<string, Dictionary<string, Dictionary<string, NavNode>>> Leaves = new()
        {
            ["BIR"] = new()
            {
                ["Analytical"] = new()
                {
                    ["DailySalesSummaryReport"] = new NavNode("1.1.1", "1.1.1 Daily Sales Summary Report"),
                    ["SalePeriodReport"] = new NavNode("1.1.2", "1.1.2 Sale Period Report"),
                    ["SalesContributionByStep"] = new NavNode("1.1.3", "1.1.3 Sales Contribution By Step"),
                    ["SalesAnalysisActualVsTarget"] = new NavNode("1.1.6", "1.1.6 Sales Analysis Actual Vs. Target")
                },
                ["PHReports"] = new()
                {
                    ["CaseFillReport"] = new NavNode("4.1.1", "4.1.1 Case Fill Report"),
                    ["ReachReport"] = new NavNode("4.1.2", "4.1.2 Reach Report")
                },
                ["PHPerfect"] = new()
                {
                    ["FCSPlus"] = new NavNode("5.1.7", "5.1.7 FCS+ Report")
                },
                ["CustomViews"] = new()
                {
                    ["DTDashboard"] = new NavNode("2.1.3", "2.1.3 DT Dashboard")
                }
            },
            ["MD"] = new()
            {
                ["General"] = new()
                {
                    ["OutletMasterTracking"] = new NavNode("4.5.43", "4.5.43 Outlet Master Tracking"),
                    ["SalesmanMasterTracking"] = new NavNode("4.5.44", "4.5.44 Salesman Master Tracking")
                }
            },
            ["RP"] = new()
            {
                ["Sales"] = new()
                {
                    ["SalesmanDailySalesReportRPT07"] = new NavNode("7.2.1", "7.2.1 Salesman Daily Sales Report (RPT07)"),
                    ["InvoiceSummaryReportRPT08"] = new NavNode("7.2.2", "7.2.2 Invoice Summary Report (RPT08)")
                },
                ["Master"] = new()
                {
                    ["SalesmanMasterReportRPT22"] = new NavNode("7.4.1", "7.4.1 Salesman Master Report (RPT22)"),
                    ["ArticleMasterReportRPT23"] = new NavNode("7.4.2", "7.4.2 Article Master Report (RPT23)"),
                    ["OutletMasterReportRPT24"] = new NavNode("7.4.3", "7.4.3 Outlet Master Report (RPT24)"),
                    ["OutletMasterStatusTrackReport"] = new NavNode("7.4.9", "7.4.9 Outlet Master Status Track Report")
                },
                ["PromotionReports"] = new()
                {
                    ["PromotionMasterReport"] = new NavNode("7.8.1", "8.8.1 Promotion Master Report"),
                    ["PromotionsPromotionAllocationReport"] = new NavNode("7.8.2", "7.8.2 Promotions - Promotion Allocation Report"),
                    ["AuditTrailReport"] = new NavNode("7.8.3", "7.8.3 Audit Trail Report"),
                    ["TradeDealMonitoringReport"] = new NavNode("7.8.4", "7.8.4 Trade Deal Monitoring Report"),
                    ["GisAuditReport"] = new NavNode("7.8.5", "7.8.5 GIS Audit Report"),
                    ["OutletSubtypeThresholdReport"] = new NavNode("7.8.10", "7.8.10 Outlet Subtype Threshold report")
                },
                ["IntegratedPromotionsReports"] = new()
                {
                    ["PromotionMasterReport"] = new NavNode("7.9.1", "7.9.1 Promotion Master Report"),
                    ["PromotionSchemeWithSalesmanAndInvoice"] = new NavNode("7.9.2", "7.9.2 Promotion Scheme with Salesman and Invoice"),
                    ["SalesOrderVsPromotionsReport"] = new NavNode("7.9.3", "7.9.3 Sales order vs Promotions report"),
                    ["PromotionTrackingReport"] = new NavNode("7.9.5", "7.9.5 Promotion Tracking Report"),
                    ["IntegratedPromotionBudgetAllocationReport"] = new NavNode("7.9.6", "7.9.6 Integrated Promotion Budget Allocation Report")
                }
            }
        };
    }
}
