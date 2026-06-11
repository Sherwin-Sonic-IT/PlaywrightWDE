// using Microsoft.Playwright;
// using System;
// using System.Collections.Generic;
// using PlaywrightWDE.Global.Navigation;

// namespace PlaywrightWDE.Global.Selectors
// {
//     public static class HPCNavLinkSelectors
//     {
//         public static readonly Dictionary<string, NavNode> Parents = new()
//         {
//             ["PJP"] = new NavNode("PJP", "4.3 PJP")
//         };

//         public static readonly Dictionary<string, Dictionary<string, NavNode>> Children = new()
//         {
//             ["PJP"] = new()
//             {
//                 ["Define Sales Route"] = new NavNode("DSR", "4.3.1 Define Sales Route (PJP01)"),
//                 ["PJP Master - Create"] = new NavNode("PJPMCr", "4.3.2 PJP Master - Create (PJP02)"),
//                 ["PJP Master - Change"] = new NavNode("PJPMCh", "4.3.3 PJP Master - Change (PJP03)"),
//                 ["Assign PJPP to Salesman-Create"] = new NavNode("APJP", "4.3.4 Assign to Salesman-Create/Change"),
//                 ["PJP Detail Generation"] = new NavNode("PJPDG", "4.3.6 PJP Detail Generation (PJP06)"),
//                 ["Manual PJP Upload"] = new NavNode("MPU", "4.3.7 Manual PJP Upload"),
//                 ["Zone Master Create/Display"] = new NavNode("ZMCD", "4.3.9 Zone Master Create/Display"),
//                 ["PJP Delivery Team Master"] = new NavNode("PJPTM", "4.3.10 PJP Delivery Team Master"),
//                 ["Sales Route Upload"] = new NavNode("SRU", "4.3.11 Sales Route Upload"),
//                 ["Routes Likely to be inactived"] = new NavNode("RLBI", "4.3.16 Routes Likely to be inactived")
//             }
//         };
//     }
// }




using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Navigation;

namespace PlaywrightWDE.Global.Selectors
{
    public static class HPCNavLinkSelectors
    {
        public static readonly Dictionary<string, NavNode> Parents = new()
        {
            ["PJP"] = new NavNode("PJP", "4.3 PJP")
        };

        public static readonly Dictionary<string, Dictionary<string, NavNode>> Children = new()
        {
            ["PJP"] = new()
            {
                ["Define Sales Route"] = new NavNode("DSR", "4.3.1 Define Sales Route (PJP01)"),
                ["PJP Master - Create"] = new NavNode("PJPMCr", "4.3.2 PJP Master - Create (PJP02)"),
                ["PJP Master - Change"] = new NavNode("PJPMCh", "4.3.3 PJP Master - Change (PJP03)"),
                ["Assign PJPP to Salesman-Create"] = new NavNode("APJP", "4.3.4 Assign to Salesman-Create/Change"),
                ["PJP Detail Generation"] = new NavNode("PJPDG", "4.3.6 PJP Detail Generation (PJP06)"),
                ["Manual PJP Upload"] = new NavNode("MPU", "4.3.7 Manual PJP Upload"),
                ["Zone Master Create/Display"] = new NavNode("ZMCD", "4.3.9 Zone Master Create/Display"),
                ["PJP Delivery Team Master"] = new NavNode("PJPTM", "4.3.10 PJP Delivery Team Master"),
                ["Sales Route Upload"] = new NavNode("SRU", "4.3.11 Sales Route Upload"),
                ["Routes Likely to be inactived"] = new NavNode("RLBI", "4.3.16 Routes Likely to be inactived")
            }
        };
    }
}

