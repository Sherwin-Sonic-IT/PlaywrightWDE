using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors
{
    public static class ManualPJPUploadEntrySelector
    {
       public record ButtonEntry(string Selector, string DefaultValue);

       public static class ManualPJPUploadRadioButtons {
             public static readonly ButtonEntry Download = new ButtonEntry("#WD55", "Download");
        }

        public static class ManualPJPUploadButtons
        {
            public static readonly ButtonEntry Submit = new ButtonEntry("div.lsButton:has-text('Submit')", "Submit");
            public static readonly ButtonEntry Download = new ButtonEntry("div.lsButton:has-text('Download')", "Download");
        }

    }
}