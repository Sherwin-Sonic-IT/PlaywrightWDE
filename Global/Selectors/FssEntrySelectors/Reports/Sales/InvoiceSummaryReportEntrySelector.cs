


// using Microsoft.Playwright;
// using System;

// namespace PlaywrightWDE.Global.Selectors
// {
//     public static class InvoiceSummaryReportEntrySelector
//     {
//         public record FieldEntry(string Selector, string DefaultValue);
//         public record ButtonEntry(string Selector, string DefaultValue);

//         public static class InvoiceSummaryReportRepFields
//         {
//             public static readonly FieldEntry SiteField =
//                 new("input[title='Site']", "");

//             public static readonly FieldEntry InvoiceDateFromField =
//                 new("input[lsdata*='S_FKDAT-LOW']", DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));

//             public static readonly FieldEntry SalesmanCodeFromField = new("#M0\\:46\\:\\:\\:3\\:34", "");
//             public static readonly FieldEntry SalesmanCodeToField   = new("#M0\\:46\\:\\:\\:3\\:59", "");

//         }

//         public static class InvoiceSummaryReportButtons
//         {

//             public static readonly ButtonEntry SalesmanCodeFromF4Button =
//                 new("span#ls-inputfieldhelpbutton[title='Salesman Code']", "");

//             public static readonly ButtonEntry SalesmanCodeToF4Button =
//                 new("span#ls-inputfieldhelpbutton[title='Salesman Code']", "");
            
//             public static readonly ButtonEntry FirstRowCheckbox =
//                 new("table#SHresultgrid1-mrss-cont-left-content tbody tr:first-child div.urSTSCOuterDiv", "");

//             public static readonly ButtonEntry LastRowCheckbox =
//                 new("table#SHresultgrid1-mrss-cont-left-content tbody tr:last-child div.urSTSCOuterDiv", "");

//             public static readonly ButtonEntry CopyButton = new("span#btnSH1_copy-r", "");
//         }
//     }
// }