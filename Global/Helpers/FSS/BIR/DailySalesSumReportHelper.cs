
// using Microsoft.Playwright;
// using PlaywrightWDE.Global.Selectors;
// using PlaywrightWDE.Global.Navigation;
// using PlaywrightWDE.VariableEntry;
// using PlaywrightWDE.Global.Logs;

// namespace PlaywrightWDE.Global.Helpers
// {
//     public static class AnalyticalReportHelper
//     {

//         public static async Task ExecuteBIRDailySalesSumReportAsync(IPage page)
//         {
//             await BIRAnalyticalReportVariableEntry.EnterFieldAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.CountryField.Selector,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.CountryField.DefaultValue
//             );

//             await BIRAnalyticalReportVariableEntry.EnterFieldAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.SiteField.Selector,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.SiteField.DefaultValue
//             );

//            await BIRAnalyticalReportVariableEntry.EnterDistributorSitesAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.DistributorSites
//             );

//             await BIRAnalyticalReportVariableEntry.EnterFieldAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.CalendarField.Selector,
//                 AnalyticalReportSelector.DailySalesSummaryRepFields.CalendarField.DefaultValue
//             );

//             await BIRAnalyticalReportVariableEntry.ClickOkAsync(page);

//             await BIRAnalyticalReportVariableEntry.SortByVerticalAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepChangeDrillDown.CalendarDay.Selector,
//                 AnalyticalReportSelector.DailySalesSummaryRepChangeDrillDown.CalendarDay.DisplayName
//             );

//             await BIRAnalyticalReportVariableEntry.SortByVerticalAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepChangeDrillDown.InvoiceDueDate.Selector,
//                 AnalyticalReportSelector.DailySalesSummaryRepChangeDrillDown.InvoiceDueDate.DisplayName
//             );

//             await BIRAnalyticalReportVariableEntry.SortByVerticalAsync(
//                 page,
//                 AnalyticalReportSelector.DailySalesSummaryRepChangeDrillDown.InvoiceNo.Selector,
//                 AnalyticalReportSelector.DailySalesSummaryRepChangeDrillDown.InvoiceNo.DisplayName
//             );

//            await BIRAnalyticalReportVariableEntry.ExportToExcelAsync(page);

//         }

//     }
// }










using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    public static class DailySalesSumReportHelper
    {
        
          

        public static async Task<string> ExecuteBIRDailySalesSumReportAsync(IPage page)
        {
              var frame = await IFrameHelper.GetDashboardReportIFrameAsync(page)
                   ?? throw new Exception("❌ Report frame not found");

            await DailySalesSummaryReportEntry.EnterFieldAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepFields.CountryField.Selector,
                DailySalesSummarySelector.DailySalesSummaryRepFields.CountryField.DefaultValue);

            await DailySalesSummaryReportEntry.EnterFieldAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepFields.SiteField.Selector,
                DailySalesSummarySelector.DailySalesSummaryRepFields.SiteField.DefaultValue);

            await DailySalesSummaryReportEntry.EnterDistributorSitesAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepFields.DistributorSites);

            await DailySalesSummaryReportEntry.EnterFieldAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepFields.CalendarField.Selector,
                DailySalesSummarySelector.DailySalesSummaryRepFields.CalendarField.DefaultValue);

            await DailySalesSummaryReportEntry.ClickOkAsync(frame);

            await DailySalesSummaryReportEntry.SortByVerticalAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepChangeDrillDown.CalendarDay.Selector,
                DailySalesSummarySelector.DailySalesSummaryRepChangeDrillDown.CalendarDay.DisplayName);

            await DailySalesSummaryReportEntry.SortByVerticalAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepChangeDrillDown.InvoiceDueDate.Selector,
                DailySalesSummarySelector.DailySalesSummaryRepChangeDrillDown.InvoiceDueDate.DisplayName);

            await DailySalesSummaryReportEntry.SortByVerticalAsync(
                frame,
                DailySalesSummarySelector.DailySalesSummaryRepChangeDrillDown.InvoiceNo.Selector,
                DailySalesSummarySelector.DailySalesSummaryRepChangeDrillDown.InvoiceNo.DisplayName);

           return await DailySalesSummaryReportEntry.ExportToExcelAsync(frame);
        }
    }
}


