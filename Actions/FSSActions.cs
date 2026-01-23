
// using Microsoft.Playwright;
// using System;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Helpers;
// using PlaywrightWDE.Global.Navigation;
// using PlaywrightWDE.Global.Selectors;


// namespace PlaywrightWDE.Actions {

//     public static class FSSActions
//     {
//         public static async Task ExecuteFssReportAsync(
//             IPage page,
//             string parentArg,
//             string childArg,
//             NavNode leaf,
//             string[] reportPath)
//         {
//             if (page == null) throw new ArgumentNullException(nameof(page));
//             if (string.IsNullOrWhiteSpace(parentArg)) throw new ArgumentNullException(nameof(parentArg));
//             if (string.IsNullOrWhiteSpace(childArg)) throw new ArgumentNullException(nameof(childArg));
//             if (reportPath == null || reportPath.Length == 0) throw new ArgumentNullException(nameof(reportPath));

//             await ClickNavLinks.ClickNavLinksAsync(page, reportPath);

//             if (parentArg.Equals(FSSNavLinkSelectors.Parents.BIR.Key, StringComparison.OrdinalIgnoreCase))
//             {
//                 switch (childArg.ToUpper())
//                 {
//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.BIR.Analytical.Key, StringComparison.OrdinalIgnoreCase):
//                         await DailySalesSumReportHelper.ExecuteBIRDailySalesSumReportAsync(page);
//                         break;

//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.BIR.PHReports.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.BIR.PHPerfect.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.BIR.CustomViews.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     default:
//                         throw new Exception($"Unknown BIR child report: {childArg}");
//                 }
//             }
//             else if (parentArg.Equals(FSSNavLinkSelectors.Parents.MD.Key, StringComparison.OrdinalIgnoreCase))
//             {
//                 switch (childArg.ToUpper())
//                 {
//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.MD.General.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     default:
//                         throw new Exception($"Unknown MD child: {childArg}");
//                 }
//             }
//             else if(parentArg.Equals(FSSNavLinkSelectors.Parents.RP.Key, StringComparison.OrdinalIgnoreCase))
//             {
//                 switch (childArg.ToUpper())
//                 {
//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.RP.Sales.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.RP.Master.Key, StringComparison.OrdinalIgnoreCase):
//                         // await SalesmanMasterReportHelper.ExecuteSalesmanMasterReportAsync(page);
//                         await ArticleMasterReportHelper.ExecuteArticleMasterReportAsync(page);
//                         // await OutletMasterReportHelper.ExecuteOutletMasterReportAsync(page);
//                         break;

//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.RP.PromotionReports.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     case var c when c.Equals(FSSNavLinkSelectors.Childrens.RP.IntegratedPromotionsReports.Key, StringComparison.OrdinalIgnoreCase):
//                         break;

//                     default:
//                         throw new Exception($"Unknown RP child: {childArg}");
//                 }
//             }
//             else
//             {   
//                 throw new Exception($"Unknown parent report: {parentArg}");
//             }
    
//         }

//     }

// }










using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;

namespace PlaywrightWDE.Actions
{
    public static class FSSActions
    {
        /// <summary>
        /// Executes a FSS report based on parent, child, and leaf.
        /// Always grabs a fresh iframe for each report to prevent "Frame was detached".
        /// </summary>
        public static async Task ExecuteFssReportAsync(
            IPage page,
            string parentArg,
            string childArg,
            NavNode leaf,
            string[] reportPath)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (string.IsNullOrWhiteSpace(parentArg)) throw new ArgumentNullException(nameof(parentArg));
            if (string.IsNullOrWhiteSpace(childArg)) throw new ArgumentNullException(nameof(childArg));
            if (reportPath == null || reportPath.Length == 0) throw new ArgumentNullException(nameof(reportPath));

            await ClickNavLinks.ClickNavLinksAsync(page, reportPath);

            if (parentArg.Equals(FSSNavLinkSelectors.Parents.RP.Key, StringComparison.OrdinalIgnoreCase) &&
                childArg.Equals(FSSNavLinkSelectors.Childrens.RP.Master.Key, StringComparison.OrdinalIgnoreCase))
            {
                switch (leaf.Key)
                {
                    case "7.4.1":
                        await SalesmanMasterReportHelper.ExecuteSalesmanMasterReportAsync(page);
                        break;

                    case "7.4.2":
                        await ArticleMasterReportHelper.ExecuteArticleMasterReportAsync(page);
                        break;

                    case "7.4.3":
                        await OutletMasterReportHelper.ExecuteOutletMasterReportAsync(page);
                        break;

                    default:
                        Logger.Log($"⚠️ Skipping unknown RP Master leaf: {leaf.Key} - {leaf.Display}");
                        return;
                }
            }
            else if (parentArg.Equals(FSSNavLinkSelectors.Parents.BIR.Key, StringComparison.OrdinalIgnoreCase))
            {
                switch (childArg.ToUpper())
                {
                    case var c when c.Equals(FSSNavLinkSelectors.Childrens.BIR.Analytical.Key, StringComparison.OrdinalIgnoreCase):
                        await DailySalesSumReportHelper.ExecuteBIRDailySalesSumReportAsync(page);
                        break;

                    default:
                        throw new Exception($"Unknown BIR child report: {childArg}");
                }
            }
            else if (parentArg.Equals(FSSNavLinkSelectors.Parents.MD.Key, StringComparison.OrdinalIgnoreCase))
            {
                switch (childArg.ToUpper())
                {
                    case var c when c.Equals(FSSNavLinkSelectors.Childrens.MD.General.Key, StringComparison.OrdinalIgnoreCase):
                        break;

                    default:
                        throw new Exception($"Unknown MD child: {childArg}");
                }
            }
            else
            {
                throw new Exception($"Unknown parent report: {parentArg}");
            }

            Logger.Log($"Completed execution of report: {leaf.Display}");

        }
    }
}
