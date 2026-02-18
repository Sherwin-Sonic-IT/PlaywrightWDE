using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;

public static class FIGLNavLinksActionDict
{
    public static readonly Dictionary<string, string> Parents =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { FIGLNavLinkSelectors.Parents["RP"].Key, FIGLNavLinkSelectors.Parents["RP"].Display }
        };

    public static readonly Dictionary<string, Dictionary<string, string>> Children =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FIGLNavLinkSelectors.Parents["RP"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FIGLNavLinkSelectors.Children["RP"]["Procurement"].Key,
                      FIGLNavLinkSelectors.Children["RP"]["Procurement"].Display }
                }
            }
        };

    public static readonly Dictionary<string, Dictionary<string, NavNode[]>> Leaves =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FIGLNavLinkSelectors.Parents["RP"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FIGLNavLinkSelectors.Children["RP"]["Procurement"].Key,
                        new[] { FIGLNavLinkSelectors.Leaves["RP"]["Procurement"]["GoodReceiptsReport"] }
                    }
                }
            }
        };
}
