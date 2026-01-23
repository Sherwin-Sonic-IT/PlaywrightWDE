using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;

public static class FIGLNavLinksActionDict {

    public static readonly Dictionary<string, string> Parents =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { FIGLNavLinkSelectors.Parents.RP.Key, FIGLNavLinkSelectors.Parents.RP.Display },
        };

    public static readonly Dictionary<string, Dictionary<string, string>> Children =
        new(StringComparer.OrdinalIgnoreCase)
         {
             {
                FIGLNavLinkSelectors.Parents.RP.Key,
                new Dictionary<string, string>
                {
                    { FIGLNavLinkSelectors.Childrens.RP.Procurement.Key, FIGLNavLinkSelectors.Childrens.RP.Procurement.Display },
                }
            }
        };

    public static readonly Dictionary<string, Dictionary<string, NavNode[]>> Leaves =
        new(StringComparer.OrdinalIgnoreCase)
         {
            {
                FIGLNavLinkSelectors.Parents.RP.Key,
                new Dictionary<string, NavNode[]>
                {
                    {
                        FIGLNavLinkSelectors.Childrens.RP.Procurement.Key,
                        new[] { FIGLNavLinkSelectors.Leaves.RP.GoodReceiptsReport }
                    }
                }
            }
        };


}