using Microsoft.Playwright;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Helpers {

    public static class GoodReceiptsReportHelper {

        public static async Task<string> ExecuteGoodReceiptsReportAsync(IPage page) {

            var frame = await IFrameHelper.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Good receipts report frame not found");

            await GoodReceiptsReportEntry.EnterFieldAsync(
            frame,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.StorageLocation.Selector,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.StorageLocation.DefaultValue);

            await GoodReceiptsReportEntry.EnterFieldAsync(
            frame,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.MovementType.Selector,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.MovementType.DefaultValue);

            await GoodReceiptsReportEntry.EnterFieldAsync(
            frame,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.PostingDate.Selector,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.PostingDate.DefaultValue);

            await GoodReceiptsReportEntry.EnterFieldAsync(
            frame,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.TransEventType.Selector,
            GoodReceiptsReportSelector.GoodReceiptsReportFields.TransEventType.DefaultValue);

            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await GoodReceiptsReportEntry.ClickDetailListAsync(frame);
            await Task.Delay(25000);
            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame);
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await CommonEntryActions.EnterCustomFileNameAsync(frame, "GoodReceiptsReport");

            return await CommonEntryActions.ClickOkAsync(page, frame);

        }
    }
}






