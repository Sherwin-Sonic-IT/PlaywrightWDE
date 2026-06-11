
//////////// ----------- ORIGINAL --------------////////////

// using Microsoft.Playwright;
// using System;
// using System.Threading.Tasks;
// using PlaywrightWDE.Global.Logs;
// using PlaywrightWDE.Global.Selectors;

// namespace PlaywrightWDE.Global.Helpers
// {
//     public static class CommonEntryHelpers
//     {
//         //  public const int DefaultTimeout = 60_000;
//         public const int DefaultTimeout = 900_000;

//          public static readonly string[] Sites = { "4049", "4A48", "4B48", "4C48", "4536", "4537" };

//         public enum ReportType
//         {
//             SalesmanMaster,
//             ArticleMaster,
//             OutletMaster,
//             SalesDailySales,
//             InvoiceSummary,
//             SalesOrderSummary
//         }


//         public static async Task FillAsync(
//             IFrame frame,
//             string selector,
//             string value,
//             string log,
//             int timeout = DefaultTimeout)
//         {
//             var el = frame.Locator(selector).First;

//             await el.FillAsync(value, new()
//             {
//                 Timeout = timeout
//             });

//             Logger.Log(log);
//         }

//         public static async Task ClickAsync(
//             IFrame frame,
//             string selector,
//             string log,
//             int timeout = DefaultTimeout)
//         {
//             var el = frame.Locator(selector).First;

//             await el.ClickAsync(new()
//             {
//                 Timeout = timeout
//             });

//             Logger.Log(log);
//         }

//         public static async Task HoverAsync(
//             IFrame frame,
//             string? selector = null,
//             AriaRole? role = null,
//             string? name = null,
//             string log = "",
//             int timeout = DefaultTimeout)
//         {
//             ILocator el;

//             if (role.HasValue && !string.IsNullOrEmpty(name))
//             {
//                 el = frame.GetByRole(role.Value, new() { Name = name });
//             }
//             else if (!string.IsNullOrEmpty(selector))
//             {
//                 el = frame.Locator(selector).First;
//             }
//             else
//             {
//                 throw new ArgumentException("Either selector or role+name must be provided.");
//             }

//             await el.HoverAsync(new()
//             {
//                 Timeout = timeout
//             });

//             Logger.Log(log);
//         }

//         public static async Task ScrollSapF4ToBottomAsync(IFrame frame, string selector)
//         {
//             var firstRow = frame
//                 .Locator(selector)
//                 .First;

//             await firstRow.WaitForAsync(new() { State = WaitForSelectorState.Visible });

//             await firstRow.FocusAsync();

//             for (int i = 0; i < 10; i++)
//             {
//                 await firstRow.PressAsync("PageDown");
//                 await frame.WaitForTimeoutAsync(200);
//             }
//         }

//     }

// }



using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Logs;
using PlaywrightWDE.Global.Selectors;

namespace PlaywrightWDE.Global.Helpers
{
    public static class CommonEntryHelpers
    {
        public const int DefaultTimeout = 600_000;

        public static readonly string[] Sites =
        {
            "4049",
            "4A48",
            "4B48",
            "4C48",
            "4536",
            "4537"
        };

        public enum ReportType
        {
            SalesmanMaster,
            ArticleMaster,
            OutletMaster,
            SalesDailySales,
            InvoiceSummary,
            SalesOrderSummary
        }

        /// <summary>
        /// Wait until element becomes visible and enabled.
        /// Infinite retry for SAP instability/network interruption.
        /// </summary>
        private static async Task WaitUntilReadyAsync(ILocator el)
        {
            int retry = 0;

            while (true)
            {
                try
                {
                    await el.WaitForAsync(new()
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 5000
                    });

                    if (await el.IsEnabledAsync())
                    {
                        return;
                    }
                }
                catch
                {
                    retry++;

                    Logger.Log($"⏳ Waiting for element... Retry {retry}");

                    await Task.Delay(3000);
                }
            }
        }

        public static async Task FillAsync(
            IFrame frame,
            string selector,
            string value,
            string log,
            int timeout = DefaultTimeout)
        {
            var el = frame.Locator(selector).First;

            await WaitUntilReadyAsync(el);

            int retry = 0;

            while (true)
            {
                try
                {
                    await el.FillAsync(value, new()
                    {
                        Timeout = 5000
                    });

                    Logger.Log(log);

                    return;
                }
                catch
                {
                    retry++;

                    Logger.Log($"⏳ Fill retry {retry}");

                    await Task.Delay(3000);
                }
            }
        }

        public static async Task ClickAsync(
            IFrame frame,
            string selector,
            string log,
            int timeout = DefaultTimeout)
        {
            var el = frame.Locator(selector).First;

            await WaitUntilReadyAsync(el);

            int retry = 0;

            while (true)
            {
                try
                {
                    await el.ClickAsync(new()
                    {
                        Timeout = 5000
                    });

                    Logger.Log(log);

                    return;
                }
                catch
                {
                    retry++;

                    Logger.Log($"⏳ Click retry {retry}");

                    await Task.Delay(3000);
                }
            }
        }

        public static async Task HoverAsync(
            IFrame frame,
            string? selector = null,
            AriaRole? role = null,
            string? name = null,
            string log = "",
            int timeout = DefaultTimeout)
        {
            ILocator el;

            if (role.HasValue && !string.IsNullOrEmpty(name))
            {
                el = frame.GetByRole(role.Value, new() { Name = name });
            }
            else if (!string.IsNullOrEmpty(selector))
            {
                el = frame.Locator(selector).First;
            }
            else
            {
                throw new ArgumentException(
                    "Either selector or role+name must be provided.");
            }

            await WaitUntilReadyAsync(el);

            int retry = 0;

            while (true)
            {
                try
                {
                    await el.HoverAsync(new()
                    {
                        Timeout = 5000
                    });

                    Logger.Log(log);

                    return;
                }
                catch
                {
                    retry++;

                    Logger.Log($"⏳ Hover retry {retry}");

                    await Task.Delay(3000);
                }
            }
        }

        public static async Task ScrollSapF4ToBottomAsync(
            IFrame frame,
            string selector)
        {
            var firstRow = frame
                .Locator(selector)
                .First;

            await WaitUntilReadyAsync(firstRow);

            int retry = 0;

            while (true)
            {
                try
                {
                    await firstRow.FocusAsync();

                    for (int i = 0; i < 10; i++)
                    {
                        await firstRow.PressAsync("PageDown");

                        await frame.WaitForTimeoutAsync(200);
                    }

                    Logger.Log("✅ SAP F4 scroll completed");

                    return;
                }
                catch
                {
                    retry++;

                    Logger.Log($"⏳ Scroll retry {retry}");

                    await Task.Delay(3000);
                }
            }
        }
    }
}


