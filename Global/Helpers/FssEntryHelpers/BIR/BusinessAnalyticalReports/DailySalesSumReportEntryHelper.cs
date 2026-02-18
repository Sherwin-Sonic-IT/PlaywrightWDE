
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;

namespace PlaywrightWDE.Global.Helpers
{
    public static class DailySalesSumReportEntryHelper
    {
        
        public static async Task<string> ExecuteBIRDailySalesSumReportAsync(IPage page)
        {
              var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                   ?? throw new Exception("❌ Report frame not found");

            await DailySalesSummaryReportEntryActions.EnterFieldAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.CountryField.Selector,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.CountryField.DefaultValue);

            await DailySalesSummaryReportEntryActions.EnterFieldAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.SiteField.Selector,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.SiteField.DefaultValue);

            await DailySalesSummaryReportEntryActions.EnterDistributorSitesAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.DistributorSites);

            await DailySalesSummaryReportEntryActions.EnterFieldAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.CalendarField.Selector,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepFields.CalendarField.DefaultValue);

            await DailySalesSummaryReportEntryActions.ClickOkAsync(frame);

            await DailySalesSummaryReportEntryActions.SortByVerticalAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepChangeDrillDown.CalendarDay.Selector,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepChangeDrillDown.CalendarDay.DisplayName);

            await DailySalesSummaryReportEntryActions.SortByVerticalAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepChangeDrillDown.InvoiceDueDate.Selector,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepChangeDrillDown.InvoiceDueDate.DisplayName);

            await DailySalesSummaryReportEntryActions.SortByVerticalAsync(
                frame,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepChangeDrillDown.InvoiceNo.Selector,
                DailySalesSummaryReportEntrySelector.DailySalesSummaryRepChangeDrillDown.InvoiceNo.DisplayName);

           return await DailySalesSummaryReportEntryActions.ExportToExcelAsync(frame);
        }
    }
}


