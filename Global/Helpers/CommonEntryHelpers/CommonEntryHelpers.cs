

using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Logs;
using PlaywrightWDE.Global.Selectors;

namespace PlaywrightWDE.Global.Helpers
{
    public static class CommonEntryHelpers
    {
        private const int DefaultTimeout = 600_000;

        public enum ReportType
        {
            SalesmanMaster,
            ArticleMaster,
            OutletMaster
        }

        public static async Task FillAsync(
            IFrame frame,
            string selector,
            string value,
            string log,
            int timeout = DefaultTimeout)
        {
            var el = frame.Locator(selector).First;

            await el.FillAsync(value, new()
            {
                Timeout = timeout
            });

            Logger.Log(log);
        }

        public static async Task ClickAsync(
            IFrame frame,
            string selector,
            string log,
            int timeout = DefaultTimeout)
        {
            var el = frame.Locator(selector).First;

            await el.ClickAsync(new()
            {
                Timeout = timeout
            });

            Logger.Log(log);
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
                throw new ArgumentException("Either selector or role+name must be provided.");
            }

            await el.HoverAsync(new()
            {
                Timeout = timeout
            });

            Logger.Log(log);
        }

    }
}
