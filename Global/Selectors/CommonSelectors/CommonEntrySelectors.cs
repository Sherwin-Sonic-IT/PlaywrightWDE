
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class CommonEntrySelectors {

        public record FieldEntry(string Selector, string DefaultValue);
        public record ButtonEntry(string Selector, string DefaultValue);
        public record MenuItemEntry(string Selector, string DefaultValue);      


        public static class CommonEntryFields
        {
            public static readonly FieldEntry SiteField = new("input[title='Site']", "");
            public static readonly FieldEntry InvoiceDateField = new("input[lsdata*='S_FKDAT-LOW']", DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
            public static readonly FieldEntry DateField = new("input[lsdata*='S_ERDAT-LOW']", DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
            public static readonly FieldEntry SalesmanCodeFromField = new("#M0\\:46\\:\\:\\:3\\:34", "");
            public static readonly FieldEntry SalesmanCodeToField   = new("#M0\\:46\\:\\:\\:3\\:59", "");

             public static readonly FieldEntry CalendarField = new("#DLG_VARIABLE_vsc_cvl_VAR_4_INPUT_inp", DateTime.Now.AddDays(-1).ToString("dd.MM.yy"));
            // public static readonly FieldEntry CalendarField = new("#DLG_VARIABLE_vsc_cvl_VAR_4_INPUT_inp", "29.05.26"); // specifc date
        }

        public static class CommonEntryButtons 
        {
            public static readonly ButtonEntry ExecuteButton = new("div[title='Execute (F8)']", "");
            public static readonly ButtonEntry MoreButton = new("#Cua2OldToolbar div[id$='hiddenOpener'][title='More']", "");
            public static readonly ButtonEntry ChooseLayoutButton = new("div[title='Choose Layout... (Ctrl+F9)']", "");
            public static readonly ButtonEntry MenuButton = new("#cua2sapmenu_btn-r", "");           

            public static readonly ButtonEntry SalesmanCodeFromF4Button = new("span#ls-inputfieldhelpbutton[title='Salesman Code']", "");

            public static readonly ButtonEntry SalesmanCodeToF4Button = new("span#ls-inputfieldhelpbutton[title='Salesman Code']", "");
            
            public static readonly ButtonEntry FirstRowCheckbox = new("table#SHresultgrid1-mrss-cont-left-content tbody tr:first-child div.urSTSCOuterDiv", "");

            public static readonly ButtonEntry LastRowCheckbox = new("table#SHresultgrid1-mrss-cont-left-content tbody tr:last-child div.urSTSCOuterDiv", "");

            public static readonly ButtonEntry CopyButton = new("span#btnSH1_copy-r", "");

            public static readonly ButtonEntry ScrollSap4ToBottom = new("table#SHresultgrid1-mrss-cont-left-content tbody tr", "");
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

