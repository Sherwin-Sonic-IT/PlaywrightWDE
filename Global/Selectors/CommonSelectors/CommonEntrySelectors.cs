
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class CommonEntrySelectors {

        public record FieldEntry(string Selector, string DefaultValue);
        public record ButtonEntry(string Selector, string DefaultValue);
        public record MenuItemEntry(string Selector, string DefaultValue);      

        public static class CommonEntryButtons 
        {
            public static readonly ButtonEntry ExecuteButton = new("div[title='Execute (F8)']", "");
            public static readonly ButtonEntry MoreButton = new("#Cua2OldToolbar div[id$='hiddenOpener'][title='More']", "");
            public static readonly ButtonEntry ChooseLayoutButton = new("div[title='Choose Layout... (Ctrl+F9)']", "");
            public static readonly ButtonEntry MenuButton = new("#cua2sapmenu_btn-r", "");           
        } 

        public static class CommonEntryMenuItems
        {
            public static readonly MenuItemEntry ChooseLayoutItem = new("tr[ct='POMNI'][id$='btn[33]-BtnMenu']", "");
            public static readonly MenuItemEntry ListItem = new("List", "");
            public static readonly MenuItemEntry ExportItem = new("//tr[td[@class='urMnuTxt' and contains(., 'Export')]]", "");
            public static readonly MenuItemEntry SpreadSheetItem = new("//tr[td[@class='urMnuTxt' and contains(., 'Spreadsheet...')]]", "");
        }

    }
}

