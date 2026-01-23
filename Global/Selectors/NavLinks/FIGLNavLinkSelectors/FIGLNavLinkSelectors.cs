
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Navigation;

namespace PlaywrightWDE.Global.Selectors {


    public static class FIGLNavLinkSelectors {

        public static class Parents {

             public static readonly NavNode RP =
                new("RP", "Reports");   

        }

        public static class Childrens {
            public static class RP {
                public static readonly NavNode Procurement = new("P", "7.1 Procurement");
            }
        }
   
        public static class Leaves {
            public static class RP
            {
                public static readonly NavNode GoodReceiptsReport = new("7.1.1", "7.1.1 Good Receipts Report - Unilever (RPT01)");
            }

        }
    }   

}


