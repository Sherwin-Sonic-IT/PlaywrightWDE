using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Selectors;

namespace PlaywrightWDE.Global.Helpers {

    public static class CommonEntryHelper {

        // Default timeout in milliseconds (10 minutes)
        private const int DefaultTimeout = 600_000;

        public static async Task FillAsync(
            IFrame frame,
            string selector,
            string value,
            string log,
            int timeout = DefaultTimeout)
        {
            var el = frame.Locator(selector).First;

            await el.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeout
            });

            await el.FillAsync(string.Empty);
            await el.FillAsync(value);
            Logger.Log(log);
        }

        public static async Task ClickAsync(
            IFrame frame,
            string selector,
            string log,
            int timeout = DefaultTimeout)
        {
            var el = frame.Locator(selector).First;

            await el.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeout
            });
            
            await el.ClickAsync();
            Logger.Log(log);
        }

        public static async Task HoverAsync(
        IFrame frame,
        string? selector = null,
        AriaRole? role = null,
        string? name = null,
        string log = "",
        WaitForSelectorState state = WaitForSelectorState.Visible,
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
                throw new ArgumentException("Either selector or role+name must be provided.");
            }

            await el.WaitForAsync(new()
            {
                State = state,
                Timeout = timeout
            });

            await el.ScrollIntoViewIfNeededAsync();

            await el.HoverAsync();

            Logger.Log(log);
        }

        // public static async Task HoverAsync(
        // IFrame frame,
        // string? selector = null,
        // AriaRole? role = null,
        // string? name = null,
        // string log = "",
        // WaitForSelectorState state = WaitForSelectorState.Visible,
        // int timeout = DefaultTimeout,
        // int postHoverDelayMs = 200)
        // {
        //     ILocator el;

        //     if (role.HasValue && !string.IsNullOrEmpty(name))
        //     {
        //         el = frame.GetByRole(role.Value, new() { Name = name });
        //     }
        //     else if (!string.IsNullOrEmpty(selector))
        //     {
        //         el = frame.Locator(selector).First;
        //     }
        //     else
        //     {
        //         throw new ArgumentException("Either selector or role+name must be provided.");
        //     }

        //     await el.WaitForAsync(new()
        //     {
        //         State = state,
        //         Timeout = timeout
        //     });

        //     await el.ScrollIntoViewIfNeededAsync();

        //     await el.HoverAsync();

        //     if (postHoverDelayMs > 0)
        //         await Task.Delay(postHoverDelayMs);

        //     Logger.Log(log);
        // }

         public static async Task ScrollSapLsScrollbarUntilVisibleAsync(
            IFrame frame,
            string targetSelector,
            int maxScrolls = 500,
            int delayMs = 25)
        {
            var nextButton =
                frame.Locator(
                    CommonEntrySelectors
                        .CommonEntryButtons
                        .VscrollNextButton
                        .Selector);

            for (int i = 0; i < maxScrolls; i++)
            {
                var target = frame.Locator(targetSelector).First;

                if (await target.IsVisibleAsync())
                {
                    Logger.Log($"✅ Element visible after {i} scrolls");
                    return;
                }

                if (!await nextButton.IsEnabledAsync())
                    break;

                await nextButton.ClickAsync();
                await Task.Delay(delayMs);
            }

            throw new TimeoutException(
                $"❌ Element '{targetSelector}' not visible after scrolling");
        }

    }
}



