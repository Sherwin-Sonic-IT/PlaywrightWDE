
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Helpers;

namespace PlaywrightWDE.Global.Entry
{
    public static class GoodReceiptsReportEntryActions
    {

        public static async Task EnterFieldAsync(
            IFrame frame,
            string selector,
            string value)
        {
            await CommonEntryHelpers.FillAsync(
                frame,
                selector,
                value,
                "✅ Field value entered");
        }

        public static async Task EnterCustomFileNameAsync(
            IFrame frame,
            string fileName)
        {
            await CommonEntryHelpers.FillAsync(
                frame,
                GoodReceiptsReportEnetrySelector
                    .GoodReceiptsReportFields
                    .FileNameField
                    .Selector,
                fileName,
                $"✅ File name save as '{fileName}'");
        }

        public static async Task ClickDetailListAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                GoodReceiptsReportEnetrySelector.GoodReceiptsReportButtons.DetailList.Selector,
                "✅ Clicked Detail List button");
        }

    }
}



