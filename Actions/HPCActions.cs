using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Helpers;
using PlaywrightWDE.Global.Navigation;


namespace PlaywrightWDE.Actions {
    
    public static class HPCActions {

    public static async Task ExecuteHpcActionsAsync(IPage page)
        {
            await ClickNavLinks.ClickNavLinksAsync(page, new[]
            {
                "1.6 Claims",
                "1.6.2 Logistics Claims Report"
            });
        }
    }
}


