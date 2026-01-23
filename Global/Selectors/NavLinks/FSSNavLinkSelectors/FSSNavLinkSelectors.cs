using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Navigation;

namespace PlaywrightWDE.Global.Selectors {

    public static class FSSNavLinkSelectors {

        public record ChangeDrillDown(string Selector, string DisplayName);
        public record FieldEntry(string Selector, string DefaultValue);

       // Detailed NavTree

        public static class NavTree
        {
            public const string DetailedNavigationTree = "#DetailedNavigationTree";
            public const string SItreeClosedFolder = "img.SItreeClosedFolder";
        }

        // Detailed NavTreeText Parents

        public static class Parents
        {
            public static readonly NavNode BIR =
                new("BIR", "Business Information Reports");

            public static readonly NavNode MD =
                new("MD", "Master Data");

            public static readonly NavNode RP =
                new("RP", "Reports");    
        }

        // Detailed NavTreeText Children

        public static class Childrens
        {
            public static class BIR
            {
                public static readonly NavNode Analytical =
                    new("AR", "1 Analytical Reports");

                public static readonly NavNode PHReports =
                    new("PR", "4.1 PH Reports");

                public static readonly NavNode PHPerfect =
                    new("PPR", "5.1 PH Perfect Reports");

                public static readonly NavNode CustomViews =
                    new("CV", "Custom Views");
            }

            public static class MD
            {
                public static readonly NavNode General =
                    new("G", "4.5 General");
            }

            public static class RP
            {
                public static readonly NavNode Sales =
                    new("S", "7.2 Sales");

                public static readonly NavNode Master =
                    new("M", "7.4 Master");

                public static readonly NavNode PromotionReports =
                    new("PR", "7.8 Promotion Reports");

                public static readonly NavNode IntegratedPromotionsReports =
                    new("IPR", "7.9 Integrated Promotions reports");    
            }
        }


        // Detailed NavTreeText Leaves

        public static class Leaves
        {
            public static class BIR
            {
                public static class Analytical
                {
                    public static readonly NavNode DailySalesSummaryReport =
                        new("1.1.1", "1.1.1 Daily Sales Summary Report");

                     public static readonly NavNode SalePeriodReport =
                        new("1.1.2", "1.1.2 Sale Period Report");
                    
                    public static readonly NavNode SalesContributionByStep = 
                        new("1.1.3", "1.1.3 Sales Contribution By Step");

                    public static readonly NavNode SalesAnalysisActualVsTarget = 
                        new("1.1.6", "1.1.6 Sales Analysis Actual Vs. Target");
                }

                public static class PHReports
                {
                    public static readonly NavNode CaseFillReport =
                        new("4.1.1", "4.1.1 Case Fill Report");

                     public static readonly NavNode ReachReport =
                        new("4.1.2", "4.1.2 Reach Report");
                }

                public static class PHPerfect
                {
                    public static readonly NavNode FCSPlus =
                        new("5.1.7", "5.1.7 FCS+ Report");
                }

                public static class CustomViews
                {
                    public static readonly NavNode DTDashboard =
                        new("2.1.3", "2.1.3 DT Dashboard");
                }
            }

            public static class MD
            {
                public static class General
                {
                    public static readonly NavNode OutletMasterTracking =
                        new("4.5.43", "4.5.43 Outlet Master Tracking");

                    public static readonly NavNode SalesmanMasterTracking =
                        new("4.5.44", "4.5.44 Salesman Master Tracking");
                }
            }

            public static class RP
            {
                public static class Sales
                {
                    public static readonly NavNode SalesmanDailySalesReportRPT07 =
                        new("7.2.1", "7.2.1 Salesman Daily Sales Report (RPT07)");

                    public static readonly NavNode InvoiceSummaryReportRPT08 =
                        new("7.2.2", "7.2.2 Invoice Summary Report (RPT08)");
                } 

                public static class Master 
                {
                    public static readonly NavNode SalesmanMasterReportRPT22 = 
                        new("7.4.1", "7.4.1 Salesman Master Report (RPT22)");

                    public static readonly NavNode ArticleMasterReportRPT23 = 
                        new("7.4.2", "7.4.2 Article Master Report (RPT23)");

                    public static readonly NavNode OutletMasterReportRPT24 = 
                        new("7.4.3", "7.4.3 Outlet Master Report (RPT24)");

                    public static readonly NavNode OutletMasterStatusTrackReport = 
                        new("7.4.9", "7.4.9 Outlet Master Status Track Report");
                }

                public static class PromotionReports
                {
                    public static readonly NavNode PromotionMasterReport = 
                        new("7.8.1", "8.8.1 Promotion Master Report");
                    
                    public static readonly NavNode PromotionsPromotionAllocationReport = 
                        new("7.8.2", "7.8.2 Promotions - Promotion Allocation Report");

                    public static readonly NavNode AuditTrailReport = 
                        new("7.8.3", "7.8.3 Audit Trail Report");

                    public static readonly NavNode TradeDealMonitoringReport = 
                        new("7.8.4", "7.8.4 Trade Deal Monitoring Report");

                    public static readonly NavNode GisAuditReport = 
                        new("7.8.5", "7.8.5 GIS Audit Report");

                    public static readonly NavNode OutletSubtypeThresholdReport = 
                        new("7.8.10", "7.8.10 Outlet Subtype Threshold report");
                }

                public static class IntegratedPromotionsReport 
                {
                    public static readonly NavNode PromotionMasterReport = 
                        new("7.9.1", "7.9.1 Promotion Master Report");

                    public static readonly NavNode PromotionSchemeWithSalesmanAndInvoice = 
                        new("7.9.2", "7.9.2 Promotion Scheme with Salesman and Invoice");

                    public static readonly NavNode SalesOrderVsPromotionsReport = 
                        new("7.9.3", "7.9.3 Sales order vs Promotions report");
                    
                    public static readonly NavNode PromotionTrackingReport = 
                        new("7.9.5", "7.9.5 Promotion Tracking Report");

                    public static readonly NavNode IntegratedPromotionBudgetAllocationReport = 
                        new("7.9.6", "7.9.6 Integrated Promotion Budget Allocation Report");
                }

            }

        }

    }
}


