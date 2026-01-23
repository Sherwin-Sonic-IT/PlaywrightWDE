// using Microsoft.Playwright;
// using PlaywrightWDE.Global.Selectors;
// using System;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Logs;
// using PlaywrightWDE.Global.FilePath;


// namespace PlaywrightWDE.VariableEntry {

//     public static class GoodReceiptsReportVariableEntry {

//         private const int DefaultTimeout = 120_000;

//         public static async Task EnterFieldAsync(IFrame frame, string fieldSelector, string value)
//         {
//             var field = frame.Locator(fieldSelector);
//             await field.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = DefaultTimeout });
//             await field.FillAsync(value);
//         }

//         public static async Task ClickLocalFileAsync(IFrame frame)
//         {
//             var localFileItem =
//                 frame.Locator(
//                     GoodReceiptsReportSelector
//                         .GoodReceiptReportMenuItem
//                         .LocalFile
//                         .Selector);

//             await localFileItem.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = 10_000
//             });

//             await localFileItem.ClickAsync();
//             Logger.Log("✅ Clicked Local File");
//         }

//         public static async Task SelectTextWithTabsRadioButtonAsync(IFrame frame)
//         {
//             var radio = frame.Locator(GoodReceiptsReportSelector.GoodReceiptsReportRadioButtons.TextWithTabs.Selector).Filter(new() { HasText = "Text with Tabs" }).First;
            
//             await radio.ClickAsync();
//             Logger.Log("✅ 'Text with Tabs' radio button clicked");
//         }


//         public static async Task ClickContinueButtonAsync(IFrame frame)
//         {
//             var continueButton = frame.Locator(GoodReceiptsReportSelector.GoodReceiptsReportButtons.ContinueButton.Selector);

//             await continueButton.WaitForAsync(new() { State = WaitForSelectorState.Visible });

//             await continueButton.ClickAsync();

//             Logger.Log("✅ 'Continue (Enter)' button clicked");
//         }

//           public static async Task EnterCustomFileNameAsync(
//             IFrame frame,
//             string customFileName,
//             int timeout = DefaultTimeout)
//             {

//                 var inputField = frame.Locator(GoodReceiptsReportSelector.GoodReceiptsReportFields.FileNameField.Selector).First;

//                 await inputField.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = timeout });

//                 await inputField.FillAsync(""); 

//                 await inputField.FillAsync(customFileName);

//                 Logger.Log($"✅ File name save as '{customFileName}'");
//             }

//            public static async Task<string> ClickGenerateButtonAsync(IPage page, IFrame frame, int timeout = DefaultTimeout)
//             {
//                 var calendarValue =
//                     AnalyticalReportSelector
//                         .DailySalesSummaryRepFields
//                         .CalendarField
//                         .DefaultValue;

//                 if (!DateTime.TryParseExact(
//                         calendarValue,
//                         "dd.MM.yy",
//                         null,
//                         System.Globalization.DateTimeStyles.None,
//                         out var date))
//                 {
//                     date = DateTime.Now;
//                     Logger.Log("⚠️ Calendar parse failed, using today");
//                 }

//                 var exportFolder = FilePath.GetDatedExportFolder(date);
//                 var logsFolder = FilePath.GetDatedLogsFolder(date);

//                 var generateButton = frame.Locator(GoodReceiptsReportSelector.GoodReceiptsReportButtons.GenerateButton.Selector);

//                 await generateButton.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = timeout });

//                 var download = await page.RunAndWaitForDownloadAsync(
//                     () => generateButton.ClickAsync(),
//                     new() { Timeout = timeout }
//                 );

//                 var filePath = Path.Combine(exportFolder, download.SuggestedFilename);
//                 await download.SaveAsAsync(filePath);

//                 var logFilePath = Path.Combine(
//                     logsFolder,
//                     Path.GetFileNameWithoutExtension(download.SuggestedFilename) + "_log.txt"
//                 );

//                 await File.WriteAllTextAsync(
//                     logFilePath,
//                     $"Downloaded File: {download.SuggestedFilename}\nPath: {filePath}"
//                 );

//                 Logger.Log($"✅ Downloaded file: {filePath}");

//                 return filePath;
//             }



//     }
// }










using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Helpers;

namespace PlaywrightWDE.Global.Entry
{
    public static class GoodReceiptsReportEntry
    {

        public static async Task EnterFieldAsync(
            IFrame frame,
            string selector,
            string value)
        {
            await CommonEntryHelper.FillAsync(
                frame,
                selector,
                value,
                "✅ Field value entered");
        }

        public static async Task EnterCustomFileNameAsync(
            IFrame frame,
            string fileName)
        {
            await CommonEntryHelper.FillAsync(
                frame,
                GoodReceiptsReportSelector
                    .GoodReceiptsReportFields
                    .FileNameField
                    .Selector,
                fileName,
                $"✅ File name save as '{fileName}'");
        }

        public static async Task ClickDetailListAsync(IFrame frame)
        {
            await CommonEntryHelper.ClickAsync(
                frame,
                GoodReceiptsReportSelector.GoodReceiptsReportButtons.DetailList.Selector,
                "✅ Clicked Detail List button");
        }

        // public static async Task ClickLocalFileAsync(IFrame frame)
        // {
        //     await CommonEntryHelper.ClickAsync(
        //         frame,
        //         GoodReceiptsReportSelector
        //             .GoodReceiptReportMenuItem
        //             .LocalFile
        //             .Selector,
        //         "✅ Clicked Local File",
        //         timeout: 10_000);
        // }

        // public static async Task ClickContinueButtonAsync(IFrame frame)
        // {
        //     await CommonEntryHelper.ClickAsync(
        //         frame,
        //         GoodReceiptsReportSelector
        //             .GoodReceiptsReportButtons
        //             .ContinueButton
        //             .Selector,
        //         "✅ 'Continue (Enter)' button clicked");
        // }

        // public static async Task SelectTextWithTabsRadioButtonAsync(IFrame frame)
        // {
        //     var radio =
        //         frame.Locator(
        //             GoodReceiptsReportSelector
        //                 .GoodReceiptsReportRadioButtons
        //                 .TextWithTabs
        //                 .Selector)
        //             .Filter(new() { HasText = "Text with Tabs" })
        //             .First;

        //     await radio.ClickAsync();
        //     Logger.Log("✅ 'Text with Tabs' radio button clicked");
        // }

        // public static async Task<string> ClickGenerateButtonAsync(
        //     IPage page,
        //     IFrame frame)
        // {
        //     var calendarValue =
        //         DailySalesSummarySelector
        //             .DailySalesSummaryRepFields
        //             .CalendarField
        //             .DefaultValue;

        //     if (!DateTime.TryParseExact(
        //             calendarValue,
        //             "dd.MM.yy",
        //             null,
        //             System.Globalization.DateTimeStyles.None,
        //             out var date))
        //     {
        //         date = DateTime.Now;
        //         Logger.Log("⚠️ Calendar parse failed, using today");
        //     }

        //     var exportFolder = FilePath.FilePath.GetDatedExportFolder(date);
        //     var logsFolder = FilePath.FilePath.GetDatedLogsFolder(date);

        //      var download = await page.RunAndWaitForDownloadAsync(
        //         () => CommonEntryHelper.ClickAsync(
        //             frame,
        //             GoodReceiptsReportSelector
        //                 .GoodReceiptsReportButtons
        //                 .GenerateButton
        //                 .Selector,
        //             "✅ Clicked Generate"));

        //     // var download = await page.RunAndWaitForDownloadAsync(
        //     //     () => CommonEntryHelper.ClickAsync(
        //     //         frame,
        //     //         GoodReceiptsReportSelector
        //     //             .GoodReceiptsReportButtons
        //     //             .GenerateButton
        //     //             .Selector,
        //     //         "✅ Clicked Generate"),
        //     //     new() { Timeout = DefaultTimeout });

        //     var filePath = Path.Combine(exportFolder, download.SuggestedFilename);
        //     await download.SaveAsAsync(filePath);

        //     var logFilePath = Path.Combine(
        //         logsFolder,
        //         Path.GetFileNameWithoutExtension(download.SuggestedFilename) + "_log.txt");

        //     await File.WriteAllTextAsync(
        //         logFilePath,
        //         $"Downloaded File: {download.SuggestedFilename}\nPath: {filePath}");

        //     Logger.Log($"✅ Downloaded file: {filePath}");
        //     return filePath;
        // }
    }
}



