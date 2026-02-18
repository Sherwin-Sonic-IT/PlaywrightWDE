using Microsoft.Playwright;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Entry;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Helpers {

    public static class GoodReceiptsReportEntryHelper {

        public static async Task<string> ExecuteGoodReceiptsReportAsync(IPage page) {

            var frame = await IFrameHelpers.GetDashboardReportIFrameAsync(page)
                ?? throw new Exception("❌ Good receipts report frame not found");

            await GoodReceiptsReportEntryActions.EnterFieldAsync(
            frame,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.StorageLocation.Selector,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.StorageLocation.DefaultValue);

            await GoodReceiptsReportEntryActions.EnterFieldAsync(
            frame,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.MovementType.Selector,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.MovementType.DefaultValue);

            await GoodReceiptsReportEntryActions.EnterFieldAsync(
            frame,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.PostingDate.Selector,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.PostingDate.DefaultValue);

            await GoodReceiptsReportEntryActions.EnterFieldAsync(
            frame,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.TransEventType.Selector,
            GoodReceiptsReportEnetrySelector.GoodReceiptsReportFields.TransEventType.DefaultValue);

            await CommonEntryActions.ClickExecuteAsync(frame);
            await CommonEntryActions.ClickMoreAsync(frame);
            await GoodReceiptsReportEntryActions.ClickDetailListAsync(frame);
            await Task.Delay(25000);
            await CommonEntryActions.ClickMenuAsync(frame);
            await CommonEntryActions.HoverListAsync(frame);
            await CommonEntryActions.HoverExportAsync(frame);
            await CommonEntryActions.ClickSpreadsheetAsync(frame);

            await MasterReportsEntryActions.EnterCustomFileNameAsync(frame, "GOODRECEIPT_REPORT");

            return await CommonEntryActions.ClickOkAsync(page, frame, null);

        }
    }
}






