
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
    public static class MasterReportsEntryActions
    {

        public static async Task EnterCustomFileNameAsync(
            IFrame frame,
            string fileName)
        {
            await CommonEntryHelpers.FillAsync(
                frame,
                MasterReportsEntrySelector.MasterReportFields.FileNameField.Selector,
                fileName,
                $"✅ File name changed to '{fileName}'");
        }


        public static async Task SelectAllRadioButtonAsync(IFrame frame)
        {
             await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportRadioButtons.AllRadioButton.Selector,
                "✅ All radio button clicked");
        }

        public static async Task ClickFindAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.FindButton.Selector,
                "✅ Clicked find");
        }

        public static async Task SearchSelectDirectionValueAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.SearchDirectionDropdownButton.Selector,
                "✅ Clicked search direction dropdown"); 

            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportEntryMenuItems.SearchDirectionItem.Selector,
                "✅ Selected search direction item");
        }

        public static async Task ClickOkFindAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.OkFindButton.Selector,
                "✅ Clicked ok find button"
            );

            await Task.Delay(5000);
        }

        public static async Task ClickCancelEscapeButtonAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                MasterReportsEntrySelector.MasterReportButtons.CancelEscapeButton.Selector,
                "✅ Clicked cancel (escape) button"
            );
        }

        public static async Task ClickSsdiAllSalesmanAsync(IFrame frame)
            {
                await CommonEntryHelpers.ClickAsync(
                    frame,
                    MasterReportsEntrySelector.MasterReportFields.SsdiAllSalesmanField.Selector,
                    "✅ Clicked /SSDI_ALL");
            }

            public static async Task ClickSsdiAllOutletAsync(IFrame frame)
            {
                await CommonEntryHelpers.ClickAsync(
                    frame,
                    MasterReportsEntrySelector.MasterReportFields.SsdiAllOutletField.Selector,
                    "✅ Clicked /SSDI_ALL");
            }
            
            public static async Task ClickSsdiMstrArticleAsync(IFrame frame)
            {
                await CommonEntryHelpers.ClickAsync(
                    frame,
                    MasterReportsEntrySelector.MasterReportFields.SsdiMstrArticleField.Selector,
                    "✅ Clicked /SSDI MSTR");
            }

       

    }
}





