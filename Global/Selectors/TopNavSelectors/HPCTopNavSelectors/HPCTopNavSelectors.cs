
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Navigation;

namespace PlaywrightWDE.Global.Selectors
{
    public static class HPCTopNavSelectors
    {
        public record ButtonTab(string Selector, string DefaultValue);


        public static class TopNavButton
        {
           public static readonly ButtonTab MasterData = new("#navNodeAnchor_2_2", "Master Data");
        }

    }
}


