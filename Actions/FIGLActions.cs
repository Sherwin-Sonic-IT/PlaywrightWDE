using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Helpers;


namespace PlaywrightWDE.Actions {

        public static class FIGLActions {

        public static async Task ExecuteFiglReportAsync( IPage page,
            string parentArg,
            string childArg,
            NavNode leaf,
            string[] reportPath) {

            if (page == null) throw new ArgumentNullException(nameof(page));
            if (string.IsNullOrWhiteSpace(parentArg)) throw new ArgumentNullException(nameof(parentArg));
            if (string.IsNullOrWhiteSpace(childArg)) throw new ArgumentNullException(nameof(childArg));
            if (reportPath == null || reportPath.Length == 0) throw new ArgumentNullException(nameof(reportPath));

            await ClickNavLinks.ClickNavLinksAsync(page, reportPath);

            if (parentArg.Equals(FIGLNavLinkSelectors.Parents.RP.Key, StringComparison.OrdinalIgnoreCase))
            {
                switch (childArg.ToUpper())
                {
                    case var c when c.Equals(FIGLNavLinkSelectors.Childrens.RP.Procurement.Key, StringComparison.OrdinalIgnoreCase):
                        await GoodReceiptsReportHelper.ExecuteGoodReceiptsReportAsync(page);
                        break;

                    default:
                        throw new Exception($"Unknown RP child report: {childArg}");
                }
            }
        }
    }

}




