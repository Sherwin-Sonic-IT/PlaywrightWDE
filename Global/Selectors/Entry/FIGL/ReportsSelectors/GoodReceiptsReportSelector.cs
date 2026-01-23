using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace PlaywrightWDE.Global.Selectors {

    public static class GoodReceiptsReportSelector {

        public record FieldEntry(string Selector, string DefaultValue);
        public record ButtonEntry(string Selector, string DefaultValue);
        public record MenuItemEntry(string Selector, string DefaultValue);

        public static class GoodReceiptsReportFields
        {
            public static readonly FieldEntry StorageLocation = new("#M0\\:46\\:\\:\\:3\\:34", "SALE");
            public static readonly FieldEntry MovementType = new("#M0\\:46\\:\\:\\:7\\:34", "101");
            public static readonly FieldEntry PostingDate = new("#M0\\:46\\:\\:\\:13\\:34", DateTime.Now.AddDays(-1).ToString("dd.MM.yy"));
             public static readonly FieldEntry TransEventType = new("#M0\\:46\\:\\:\\:15\\:34", "WE");
             public static readonly FieldEntry FileNameField = new("#M1\\:46\\:\\:\\:1\\:12", "");
        }

        public static class GoodReceiptsReportButtons {
            public static readonly ButtonEntry ContinueButton = new("#M1\\:37\\:\\:btn\\[0\\]", "");
            public static readonly ButtonEntry GenerateButton = new("#M1\\:37\\:\\:btn\\[0\\]", "");
            public static readonly ButtonEntry DetailList = new("tr[ct='POMNI']:has-text('Detail List')", "");
        }

        public static class GoodReceiptsReportRadioButtons {
            public static readonly ButtonEntry TextWithTabs = new("span[name='%RBGROUP50397441_01'] span", "");
        }

        public static class GoodReceiptReportMenuItem 
        {
            public static readonly MenuItemEntry LocalFile = new("//tr[td[@class='urMnuTxt' and contains(., 'Local File...')]]", "");
            public static readonly FieldEntry TextWithTabs = new("span.lsField__input[ct='CBS']:has-text('/SSDI ALL')", "");
        }
    }
}

