
// using Microsoft.Playwright;
// using System;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Navigation;

// namespace PlaywrightWDE.Global.Selectors {


//     public static class FIGLNavLinkSelectors {

//         public static class Parents {

//              public static readonly NavNode RP =
//                 new("RP", "Reports");   

//         }

//         public static class Childrens {
//             public static class RP {
//                 public static readonly NavNode Procurement = new("P", "7.1 Procurement");
//             }
//         }
   
//         public static class Leaves {
//             public static class RP
//             {
//                 public static readonly NavNode GoodReceiptsReport = new("7.1.1", "7.1.1 Good Receipts Report - Unilever (RPT01)");
//             }

//         }
//     }   

// }






using Microsoft.Playwright;
using System.Collections.Generic;
using PlaywrightWDE.Global.Navigation;

namespace PlaywrightWDE.Global.Selectors
{
    public static class FIGLNavLinkSelectors
    {
      
        public static readonly Dictionary<string, NavNode> Parents = new()
        {
            ["RP"] = new NavNode("RP", "Reports")
        };

        public static readonly Dictionary<string, Dictionary<string, NavNode>> Children = new()
        {
            ["RP"] = new()
            {
                ["Procurement"] = new NavNode("P", "7.1 Procurement")
            }
        };

        public static readonly Dictionary<string, Dictionary<string, Dictionary<string, NavNode>>> Leaves = new()
        {
            ["RP"] = new()
            {
                ["Procurement"] = new()
                {
                    ["GoodReceiptsReport"] = new NavNode("7.1.1", "7.1.1 Good Receipts Report - Unilever (RPT01)")
                }
            }
        };
    }
}
