using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class DailySalesSummaryReportEntrySelector {
        
        public record ChangeDrillDown(string Selector, string DisplayName);
        public record FieldEntry(string Selector, string DefaultValue);

        public static class DailySalesSummaryRepFields
        {
            public static readonly FieldEntry CountryField  = new("#DLG_VARIABLE_vsc_cvl_VAR_1_INPUT_inp", "PH");
            public static readonly FieldEntry SiteField  = new("#DLG_VARIABLE_vsc_cvl_VAR_2_INPUT_inp", "4048");
            public static readonly FieldEntry DistributorField = new("#DLG_VARIABLE_vsc_cvl_VAR_3_INPUT_inp", "");
            public static readonly FieldEntry CalendarField = new("#DLG_VARIABLE_vsc_cvl_VAR_4_INPUT_inp", DateTime.Now.AddDays(-1).ToString("dd.MM.yy"));
            // public static readonly FieldEntry CalendarField = new("#DLG_VARIABLE_vsc_cvl_VAR_4_INPUT_inp", "21.02.26");
            public static readonly string[] DistributorSites = { "4049", "4A48", "4B48", "4C48", "4536", "4537" };
        }

        public static class DailySalesSummaryRepChangeDrillDown
        {
            public static readonly ChangeDrillDown CalendarDay = new("#NAVIGATION_PANE_ITEM_1_acQueryNavigation_navTree_unid31", "Calendar Day");

            public static readonly ChangeDrillDown InvoiceDueDate = new("#NAVIGATION_PANE_ITEM_1_acQueryNavigation_navTree_unid64", "Invoice Due Date");

            public static readonly ChangeDrillDown InvoiceNo = new("#NAVIGATION_PANE_ITEM_1_acQueryNavigation_navTree_unid70", "Invoice No");
        }

        public static class Popups
        {
            public const string SapPopupMainX0 = "#sapPopupMainId_X0";
            public const string SapPopupMainX1 = "#sapPopupMainId_X1";
            public const string SapPopupMainX2 = "#sapPopupMainId_X2";
        }

        public static class DrilldownMenuItems
        {
            public const string ChangeDrilldown = "Change Drilldown";
            public const string DrilldownBy = "Drilldown by";
            public const string Vertical = "Vertical";
        }

        public static class ExportExcelButton 
        {
            public const string ExportToExcel = "#BUTTON_GROUP_ITEM_1_btn4_acButton";
            public const string Ok = "#DLG_VARIABLE_dlgBase_BTNOK";
        }

    }
}


