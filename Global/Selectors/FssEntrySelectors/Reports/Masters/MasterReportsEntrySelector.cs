using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class MasterReportsEntrySelector {

        public record FieldEntry(string Selector, string DefaultValue);
        public record ButtonEntry(string Selector, string DefaultValue);
        public record MenuItemEntry(string Selector, string DefaultValue);

        public static class MasterReportFields {
            public static readonly FieldEntry SiteField = new("input.lsField__input[title='Site']", "");
            public static readonly FieldEntry StatusField = new("#M0\\:46\\:\\:\\:8\\:34", "2");
            public static readonly FieldEntry DeliverySite = new("#M0\\:46\\:\\:\\:5\\:34", "4C48"); 
            public static readonly FieldEntry SsdiAllSalesmanField = new("span.lsField__input[ct='CBS'][lsdata*='/SSDI ALL']", "");       
            public static readonly FieldEntry SsdiAllOutletField = new("span.lsField__input[ct='CBS'][lsdata*='/SSDI_ALL']", "");  
            public static readonly FieldEntry SsdiMstrArticleField = new("span.lsField__input[ct='CBS'][lsdata*='/SSDI_MSTR']","");
            public static readonly FieldEntry FileNameField = new("#popupDialogInputField", "");
            public static readonly FieldEntry SearchTermField = new("input[title='ALV Control: Cell Content']", "");
        }

        public static class MasterReportButtons {
            public static readonly ButtonEntry OkButton = new("#UpDownDialogChoose", "");
            public static readonly ButtonEntry FindButton = new("div[title='Find (Ctrl+F)']", "");
            public static readonly ButtonEntry SearchDirectionDropdownButton  = new("span#M2\\:46\\:\\:\\:1\\:20-btn", "");
            public static readonly ButtonEntry OkFindButton = new("div[title='OK (Enter)']", "");
            public static readonly ButtonEntry CancelEscapeButton = new("#M2\\:37\\:\\:btn\\[12\\]", "");
            
            // public static readonly ButtonEntry VscrollPrevButton291 = new("#C291_vscroll-Prev", "");
            // public static readonly ButtonEntry VscrollPrevButton121 = new("#C121_vscroll-Prev", "");
            // public static readonly ButtonEntry VscrollNextButton291 = new("#C291_vscroll-Nxt", "");
            // public static readonly ButtonEntry VscrollNextButton120 = new("#C120_vscroll-Nxt", "");
            // public static readonly ButtonEntry VscrollNextButton121 = new("#C121_vscroll-Nxt", "");
        }

        public static class MasterReportRadioButtons {
            public static readonly ButtonEntry AllRadioButton = new("span[name='%RBGROUP000258'] >> text=All", "");
        }

        public static class MasterReportEntryMenuItems {
            public static readonly MenuItemEntry SearchDirectionItem = new("div.lsListbox__values div.lsListbox__value[data-itemindex='0']", "");

        }

    }

}


