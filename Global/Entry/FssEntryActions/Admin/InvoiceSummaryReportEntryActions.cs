// using Microsoft.Playwright;
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Selectors;
// using PlaywrightWDE.Global.FilePath;
// using PlaywrightWDE.Global.Logs;
// using PlaywrightWDE.Global.Helpers;
// using System.Globalization;


// namespace PlaywrightWDE.Global.Entry {
    
//     public static class InvoiceSummaryReportEntryActions
//     {
//         public static async Task<string> SalesmanCodeFromF4Button (IFrame frame)
//         {
//             await CommonEntryHelpers.ClickAsync(
//                 frame,
//                 InvoiceSummaryReportEntrySelector.InvoiceSummaryReportButtons.SalesmanCodeFromF4Button.Selector,
//                 "✅ Clicked Salesman Button From List");

//             return "Clicked";
//         }

//        public static async Task<string> SalesmanCodeToF4Button (IFrame frame)
//         {
//             await CommonEntryHelpers.ClickAsync(
//                 frame,
//                 InvoiceSummaryReportEntrySelector.InvoiceSummaryReportButtons.SalesmanCodeToF4Button.Selector,
//                 "✅ Clicked Salesman Button To List");

//             return "Clicked";
//         }


//         public static async Task<string> SelectFirstAvailableCheckbox(IFrame frame)
//         {
//             await CommonEntryHelpers.ClickAsync(
//                 frame,
//                 InvoiceSummaryReportEntrySelector.InvoiceSummaryReportButtons.FirstRowCheckbox.Selector,
//                 "✅ Selected first available checkbox");

//             return "Clicked";
//         }

//         public static async Task<string> SelectLastAvailableCheckbox(IFrame frame)
//         {
//             await CommonEntryHelpers.ClickAsync(
//                 frame,
//                 InvoiceSummaryReportEntrySelector.InvoiceSummaryReportButtons.LastRowCheckbox.Selector,
//                 "✅ Selected last available checkbox");

//             return "Clicked";
//         }

//         public static async Task<string> ClickCopyButton(IFrame frame)
//         {
//             await CommonEntryHelpers.ClickAsync(
//                 frame,
//                 InvoiceSummaryReportEntrySelector.InvoiceSummaryReportButtons.CopyButton.Selector,
//                 "✅ Clicked Copy button");

//             return "Clicked";
//         }

//     }
// }