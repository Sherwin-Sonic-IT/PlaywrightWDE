

using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.Global.Navigation;

public static class FSSNavLinksActionsDict
{
    public static readonly Dictionary<string, string> Parents =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { FSSNavLinkSelectors.Parents["BIR"].Key, FSSNavLinkSelectors.Parents["BIR"].Display },
            { FSSNavLinkSelectors.Parents["MD"].Key,  FSSNavLinkSelectors.Parents["MD"].Display  },
            { FSSNavLinkSelectors.Parents["RP"].Key, FSSNavLinkSelectors.Parents["RP"].Display },
            { FSSNavLinkSelectors.Parents["AM"].Key, FSSNavLinkSelectors.Parents["AM"].Display }
        };

    public static readonly Dictionary<string, Dictionary<string, string>> Children =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FSSNavLinkSelectors.Parents["BIR"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["BIR"]["Analytical Reports"].Key, FSSNavLinkSelectors.Children["BIR"]["Analytical Reports"].Display },
                    { FSSNavLinkSelectors.Children["BIR"]["PH Reports"].Key,  FSSNavLinkSelectors.Children["BIR"]["PH Reports"].Display  },
                    { FSSNavLinkSelectors.Children["BIR"]["PH Perfect Reports"].Key,  FSSNavLinkSelectors.Children["BIR"]["PH Perfect Reports"].Display  },
                    { FSSNavLinkSelectors.Children["BIR"]["Custom Views"].Key, FSSNavLinkSelectors.Children["BIR"]["Custom Views"].Display }
                }
            },
            {
                FSSNavLinkSelectors.Parents["MD"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["MD"]["General"].Key, FSSNavLinkSelectors.Children["MD"]["General"].Display }
                }
            },
            {
                FSSNavLinkSelectors.Parents["RP"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["RP"]["Sales"].Key, FSSNavLinkSelectors.Children["RP"]["Sales"].Display },
                    { FSSNavLinkSelectors.Children["RP"]["Master"].Key, FSSNavLinkSelectors.Children["RP"]["Master"].Display },
                    { FSSNavLinkSelectors.Children["RP"]["Promotion Reports"].Key, FSSNavLinkSelectors.Children["RP"]["Promotion Reports"].Display },
                    { FSSNavLinkSelectors.Children["RP"]["Integrated Promotions reports"].Key, FSSNavLinkSelectors.Children["RP"]["Integrated Promotions reports"].Display }
                }
            },
            {
                FSSNavLinkSelectors.Parents["AM"].Key,
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { FSSNavLinkSelectors.Children["AM"]["10.1 Change Site"].Key, FSSNavLinkSelectors.Children["AM"]["10.1 Change Site"].Display }
                }
            }
        };

    public static readonly Dictionary<string, Dictionary<string, NavNode[]>> Leaves =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                FSSNavLinkSelectors.Parents["BIR"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FSSNavLinkSelectors.Children["BIR"]["Analytical Reports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical Reports"]["Daily Sales Summary Report"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical Reports"]["Sale Period Report"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical Reports"]["Sales Contribution By Step"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Analytical Reports"]["Sales Analysis Actual Vs. Target"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["BIR"]["PH Reports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["PH Reports"]["Case Fill Report"],
                            FSSNavLinkSelectors.Leaves["BIR"]["PH Reports"]["Reach Report"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["BIR"]["PH Perfect Reports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["PH Perfect Reports"]["FCS Plus"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["BIR"]["Custom Views"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["BIR"]["Custom Views"]["custom views"],
                            FSSNavLinkSelectors.Leaves["BIR"]["Custom Views"]["DT Dashboard"]
                        }
                    }
                }
            },
            {
                FSSNavLinkSelectors.Parents["MD"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FSSNavLinkSelectors.Children["MD"]["General"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["MD"]["General"]["Outlet Master Tracking"],
                            FSSNavLinkSelectors.Leaves["MD"]["General"]["Salesman Master Tracking"]
                        }
                    }
                }
            },
            {
                FSSNavLinkSelectors.Parents["RP"].Key,
                new Dictionary<string, NavNode[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        FSSNavLinkSelectors.Children["RP"]["Sales"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["Sales"]["Salesman Daily Sales Report (RPT07)"],
                            FSSNavLinkSelectors.Leaves["RP"]["Sales"]["Invoice Summary Report (RPT08)"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["RP"]["Master"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["Salesman Master Report (RPT22)"],
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["Article Master Report (RPT23)"],
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["Outlet Master Report (RPT24)"],
                            FSSNavLinkSelectors.Leaves["RP"]["Master"]["Outlet Master Status Track Report"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["RP"]["Promotion Reports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["Promotion Reports"]["Promotion Master Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Promotion Reports"]["Promotions Promotion Allocation Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Promotion Reports"]["Audit Trail Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Promotion Reports"]["Trade Deal Monitoring Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Promotion Reports"]["GIS Audit Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Promotion Reports"]["Outlet Subtype Threshold Report"]
                        }
                    },
                    {
                        FSSNavLinkSelectors.Children["RP"]["Integrated Promotions reports"].Key,
                        new[]
                        {
                            FSSNavLinkSelectors.Leaves["RP"]["Integrated Promotions reports"]["Promotion Master Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Integrated Promotions reports"]["Promotion Scheme with Salesman and Invoice"],
                            FSSNavLinkSelectors.Leaves["RP"]["Integrated Promotions reports"]["Sales order vs Promotions report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Integrated Promotions reports"]["Promotion Tracking Report"],
                            FSSNavLinkSelectors.Leaves["RP"]["Integrated Promotions reports"]["Integrated Promotion Budget Allocation Report"]
                        }
                    }
                }
            },
        };
}
