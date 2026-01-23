using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class MasterReportSelector {

        public record FieldEntry(string Selector, string DefaultValue);
        public record ButtonEntry(string Selector, string DefaultValue);
        public record MenuItemEntry(string Selector, string DefaultValue);

        public static class MasterReportFields {
            public static readonly FieldEntry SiteField = new("input.lsField__input[title='Site']", "4C48");
            public static readonly FieldEntry StatusField = new("#M0\\:46\\:\\:\\:8\\:34", "2");
            public static readonly FieldEntry DeliverySite = new("#M0\\:46\\:\\:\\:5\\:34", "4C48"); 
            public static readonly FieldEntry SSDIAllField = new("span.lsField__input[ct='CBS']:has-text('/SSDI ALL')", "");
            public static readonly FieldEntry SSDIICField = new("span.lsField__input[ct='CBS']:has-text('/SSDI IC')", "");
            public static readonly FieldEntry SSDIMSTR = new("span.lsField__input[ct='CBS']:has-text('/SSDI_MSTR')","");
            public static readonly FieldEntry FileNameField = new("#popupDialogInputField", "");
        }

        public static class MasterReportRadioButtons {
            public static readonly ButtonEntry AllRadioButton = new("span[name='%RBGROUP000258'] >> text=All", "");
        }

    }

}


