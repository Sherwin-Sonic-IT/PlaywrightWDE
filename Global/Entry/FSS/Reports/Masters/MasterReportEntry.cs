// using Microsoft.Playwright;
// using System;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Selectors;
// using PlaywrightWDE.Global.Logs;
// using PlaywrightWDE.Global.FilePath;


// namespace PlaywrightWDE.VariableEntry {

//     public static class MasterReportEntry {

//         private const int DefaultTimeout = 120_000;

//         public static async Task EnterFieldAsync(
//             IFrame frame,
//             string fieldSelector,
//             string value,
//             int timeout = DefaultTimeout)
//         {
//             var field = frame.Locator(fieldSelector).First;
//             await field.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = timeout });
//             await field.FillAsync(value);
//         }

//         public static async Task SelectAllRadioButtonAsync(IFrame frame, int timeout = DefaultTimeout)
//         {
//             await frame.EvaluateAsync(@"() => {
//                 const radios = Array.from(document.querySelectorAll(""span[name='%RBGROUP000258']""));
//                 const radioAll = radios.find(r => r.innerText.trim() === 'All');
//                 if (!radioAll) return;

//                 radios.forEach(r => r.setAttribute('aria-checked', r === radioAll ? 'true' : 'false'));
//                 radios.forEach(r => r.classList.toggle('lsRadioButton--checked', r === radioAll));
//                 radios.forEach(r => r.classList.toggle('lsRadioButton--unchecked', r !== radioAll));
//                 radioAll.dispatchEvent(new Event('Change', { bubbles: true }));
//             }");

//             Logger.Log("✅ 'All' radio button selected");
//         }

//         public static async Task ClickExecuteAsync(
//             IFrame frame,
//             int timeout = DefaultTimeout)
//         {
//             var executeButton =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportButtons
//                         .ExecuteButton
//                         .Selector);

//             await executeButton.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = timeout
//             });

//             await executeButton.ClickAsync();
//             Logger.Log("✅ Clicked Execute button");
//         }

//        public static async Task ClickMoreAsync(
//        IFrame frame,
//        int timeout = DefaultTimeout)
//         {
//             var moreButton =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportButtons
//                         .MoreButton
//                         .Selector);

//             await moreButton.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = timeout
//             });

//             await moreButton.ClickAsync();
//             Logger.Log("✅ Clicked More button");
//         }

//        public static async Task ClickChooseLayoutAsync(
//        IFrame frame,
//        int timeout = DefaultTimeout)
//         {
//             var chooseLayoutItem =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportMenuItem
//                         .ChooseLayoutItem
//                         .Selector);

//             await chooseLayoutItem.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = timeout
//             });

//             await chooseLayoutItem.ClickAsync();
//             Logger.Log("✅ Clicked Choose Layout");
//         }

//         public static async Task ClickSSDIAllAsync(IFrame frame)
//         {
//             var selector =
//                 MasterReportSelector
//                     .MasterReportFields
//                     .SSDIAllField
//                     .Selector;

//             await ScrollSapLsScrollbarUntilVisibleAsync(frame, selector);

//             await frame.Locator(selector).First.ClickAsync();

//             Logger.Log("✅ Clicked /SSDI ALL");
//         }

//          public static async Task ClickSSDICAsync(IFrame frame)
//         {
//             var selector =
//                 MasterReportSelector.
//                 MasterReportFields
//                 .SSDIICField
//                 .Selector;

//             await ScrollSapLsScrollbarUntilVisibleAsync(frame, selector);

//             await frame.Locator(selector).First.ClickAsync();

//             Logger.Log("✅ Clicked /SSDI IC");
//         }

//            public static async Task ClickSSDIMSTRAsync(IFrame frame)
//         {
//             var selector =
//                 MasterReportSelector.
//                 MasterReportFields
//                 .SSDIMSTR
//                 .Selector;

//             await ScrollSapLsScrollbarUntilVisibleAsync(frame, selector);

//             await frame.Locator(selector).First.ClickAsync();

//             Logger.Log("✅ Clicked /SSDI MSTR");
//         }

//         public static async Task ScrollSapLsScrollbarUntilVisibleAsync(
//             IFrame frame,
//             string targetSelector,
//             int maxScrolls = 500,
//             int delayMs = 50)
//         {
//             var nextButton =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportButtons
//                         .VscrollNextButton
//                         .Selector);

//             for (int i = 0; i < maxScrolls; i++)
//             {
//                 var target = frame.Locator(targetSelector).First;

//                 if (await target.IsVisibleAsync())
//                 {
//                     Logger.Log($"✅ Element visible after {i} scrolls");
//                     return;
//                 }

//                 if (!await nextButton.IsEnabledAsync())
//                     break;

//                 await nextButton.ClickAsync();
//                 await Task.Delay(delayMs);
//             }

//             throw new TimeoutException(
//                 $"❌ Element '{targetSelector}' not visible after scrolling");
//         }

//         public static async Task ClickMenuAsync(IFrame frame)
//         {
//             var menuButton =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportButtons
//                         .MenuButton
//                         .Selector);

//             await menuButton.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible
//             });

//             await menuButton.ClickAsync();
//              Logger.Log("✅ Clicked Menu");
//         }

//         public static async Task HoverListAsync(IFrame frame)
//         {
//             var listItem =
//                 frame.GetByRole(
//                     AriaRole.Menuitem,
//                     new()
//                     {
//                         Name = MasterReportSelector
//                             .MasterReportMenuItem
//                             .ListItem
//                             .Selector
//                     });

//             await listItem.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = 15_000
//             });

//             await listItem.HoverAsync();
//             Logger.Log("✅ Hovered List");
//         }

//         public static async Task HoverExportAsync(IFrame frame)
//         {
//             var exportItem =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportMenuItem
//                         .ExportItem
//                         .Selector);

//             await exportItem.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Attached,
//                 Timeout = 30_000
//             });

//             await exportItem.ScrollIntoViewIfNeededAsync();
//             await exportItem.HoverAsync();

//             Logger.Log("✅ Hovered Export");
//         }

//         public static async Task ClickSpreadsheetAsync(IFrame frame)
//         {
//             var spreadsheetItem =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportMenuItem
//                         .SpreadSheetItem
//                         .Selector);

//             await spreadsheetItem.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = 10_000
//             });

//             await spreadsheetItem.ClickAsync();
//             Logger.Log("✅ Clicked Spreadsheet");
//         }

//         public static async Task EnterCustomFileNameAsync(
//         IFrame frame,
//         string customFileName,
//         int timeout = DefaultTimeout)
//         {

//             var inputField = frame.Locator(MasterReportSelector.MasterReportFields.FileNameField.Selector).First;

//             await inputField.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = timeout });

//             await inputField.FillAsync(""); 

//             await inputField.FillAsync(customFileName);

//             Logger.Log($"✅ File name changed to '{customFileName}'");
//         }

//         public static async Task<string> ClickOkAsync(
//             IPage page,
//             IFrame frame,
//             int timeout = DefaultTimeout)
//         {
//             var calendarValue =
//                 DailySalesSummarySelector
//                     .DailySalesSummaryRepFields
//                     .CalendarField
//                     .DefaultValue;

//             if (!DateTime.TryParseExact(
//                     calendarValue,
//                     "dd.MM.yy",
//                     null,
//                     System.Globalization.DateTimeStyles.None,
//                     out var date))
//             {
//                 date = DateTime.Now;
//                 Logger.Log("⚠️ Calendar parse failed, using today");
//             }

//             var exportFolder = FilePath.GetDatedExportFolder(date);
//             var logsFolder = FilePath.GetDatedLogsFolder(date);

//             var okButton =
//                 frame.Locator(
//                     MasterReportSelector
//                         .MasterReportButtons
//                         .OkButton
//                         .Selector);

//             await okButton.WaitForAsync(new()
//             {
//                 State = WaitForSelectorState.Visible,
//                 Timeout = timeout
//             });

//            var download = await page.RunAndWaitForDownloadAsync(
//                 () => okButton.ClickAsync(),
//                 new() { Timeout = DefaultTimeout }  
//             );

//             var filePath =
//                 Path.Combine(exportFolder, download.SuggestedFilename);

//             await download.SaveAsAsync(filePath);

//             var logFilePath =
//                 Path.Combine(
//                     logsFolder,
//                     Path.GetFileNameWithoutExtension(download.SuggestedFilename) + "_log.txt");

//             await File.WriteAllTextAsync(
//                 logFilePath,
//                 $"Downloaded File: {download.SuggestedFilename}\nPath: {filePath}");

//             Logger.Log($"✅ Downloaded file: {filePath}");

//             return filePath;
//         }
//     }
// }










using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.FilePath;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Entry;


namespace PlaywrightWDE.Global.Entry
{
    public static class MasterReportEntry
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

        public static async Task SelectAllRadioButtonAsync(IFrame frame)
        {
            await frame.EvaluateAsync(@"() => {
                const radios = Array.from(document.querySelectorAll(""span[name='%RBGROUP000258']""));
                const radioAll = radios.find(r => r.innerText.trim() === 'All');
                if (!radioAll) return;

                radios.forEach(r => r.setAttribute('aria-checked', r === radioAll ? 'true' : 'false'));
                radios.forEach(r => r.classList.toggle('lsRadioButton--checked', r === radioAll));
                radios.forEach(r => r.classList.toggle('lsRadioButton--unchecked', r !== radioAll));
                radioAll.dispatchEvent(new Event('Change', { bubbles: true }));
            }");

            Logger.Log("✅ 'All' radio button selected");
        }

        public static async Task ClickSSDIAllAsync(IFrame frame)
        {
            await ClickWithScrollAsync(
                frame,
                MasterReportSelector.MasterReportFields.SSDIAllField.Selector,
                "✅ Clicked /SSDI ALL");
        }

        public static async Task ClickSSDICAsync(IFrame frame)
        {
            await ClickWithScrollAsync(
                frame,
                MasterReportSelector.MasterReportFields.SSDIICField.Selector,
                "✅ Clicked /SSDI IC");
        }

        public static async Task ClickSSDIMSTRAsync(IFrame frame)
        {
            await ClickWithScrollAsync(
                frame,
                MasterReportSelector.MasterReportFields.SSDIMSTR.Selector,
                "✅ Clicked /SSDI MSTR");
        }

        private static async Task ClickWithScrollAsync(
            IFrame frame,
            string selector,
            string log)
        {
            await CommonEntryHelper.ScrollSapLsScrollbarUntilVisibleAsync(frame, selector);
            await frame.Locator(selector).First.ClickAsync();
            Logger.Log(log);
        }

    }
}


