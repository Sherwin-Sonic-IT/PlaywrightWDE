using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {
    
    public static class ChangeSiteEntrySelector
    {
        public record FieldEntry(string Selector, string DefaultValue);

        public record ButtonEntry(string Selector, string DefaultValue);

        public static class ChangeSiteRepFields
        {
            public static readonly FieldEntry SiteField = new("#WD5E", "");
        }

        public static class ChangeSiteButtons
        {
            public static readonly ButtonEntry SaveButton = new("#WD1F", "Save");
        }

    }
 }
