using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class SalesmanDailySalesReportEntrySelector
    {
        public record FieldEntry(string Selector, string DefaultValue);

        public static class SalesmanDailySalesReportRepFields
        {
            public static readonly FieldEntry SiteField =  new("#M0\\:46\\:\\:\\:1\\:34", "");
            public static readonly FieldEntry InvoiceDateField = new("input[title='Billing Date for Billing Index and Printout']", DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
             public static readonly FieldEntry InvLQSsdiField =  new("span.lsField__input[lsdata*='/INV_LQ_SSDI']", "");       
        }
      
    }
} 