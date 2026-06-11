using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Navigation;
using PlaywrightWDE.Global.Selectors;

public static class HPCNavLinksActionsDict
{
    public static readonly Dictionary<string, string> Parents = new(StringComparer.OrdinalIgnoreCase)
    {
        { HPCNavLinkSelectors.Parents["PJP"].Key, HPCNavLinkSelectors.Parents["PJP"].Display }
    };

    public static readonly Dictionary<string, Dictionary<string, string>> Children = new(StringComparer.OrdinalIgnoreCase)
    {
        {
            HPCNavLinkSelectors.Parents["PJP"].Key,
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { HPCNavLinkSelectors.Children["PJP"]["Define Sales Route"].Key, HPCNavLinkSelectors.Children["PJP"]["Define Sales Route"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Master - Create"].Key, HPCNavLinkSelectors.Children["PJP"]["PJP Master - Create"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Master - Change"].Key, HPCNavLinkSelectors.Children["PJP"]["PJP Master - Change"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["Assign PJPP to Salesman-Create"].Key, HPCNavLinkSelectors.Children["PJP"]["Assign PJPP to Salesman-Create"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Detail Generation"].Key, HPCNavLinkSelectors.Children["PJP"]["PJP Detail Generation"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["Manual PJP Upload"].Key, HPCNavLinkSelectors.Children["PJP"]["Manual PJP Upload"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["Zone Master Create/Display"].Key, HPCNavLinkSelectors.Children["PJP"]["Zone Master Create/Display"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Delivery Team Master"].Key, HPCNavLinkSelectors.Children["PJP"]["PJP Delivery Team Master"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["Sales Route Upload"].Key, HPCNavLinkSelectors.Children["PJP"]["Sales Route Upload"].Display },
                { HPCNavLinkSelectors.Children["PJP"]["Routes Likely to be inactived"].Key, HPCNavLinkSelectors.Children["PJP"]["Routes Likely to be inactived"].Display }
            }
        }
    };

    public static readonly Dictionary<string, Dictionary<string, NavNode[]>> Leaves = new(StringComparer.OrdinalIgnoreCase)
    {
        {
            HPCNavLinkSelectors.Parents["PJP"].Key,
            new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
            {
                { HPCNavLinkSelectors.Children["PJP"]["Define Sales Route"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Master - Create"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Master - Change"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["Assign PJPP to Salesman-Create"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Detail Generation"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["Manual PJP Upload"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["Zone Master Create/Display"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["PJP Delivery Team Master"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["Sales Route Upload"].Key, Array.Empty<NavNode>() },
                { HPCNavLinkSelectors.Children["PJP"]["Routes Likely to be inactived"].Key, Array.Empty<NavNode>() }
            }
        }
    };
}