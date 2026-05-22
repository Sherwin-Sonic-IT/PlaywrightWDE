using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.FilePath;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Helpers;
using System.Globalization;


namespace PlaywrightWDE.Global.Entry
{
    public static class ChangeSiteEntryActions
    {
        public static async Task<string> SaveAsync(IFrame frame)
        {
            await CommonEntryHelpers.ClickAsync(
                frame,
                ChangeSiteEntrySelector.ChangeSiteButtons.SaveButton.Selector,
                "✅ Clicked Save");

            return "Saved";
        }

    }
}