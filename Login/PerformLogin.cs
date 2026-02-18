
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using PlaywrightWDE.Global.Selectors;
using PlaywrightWDE.WebURL;
using PlaywrightWDE.Global.Logs;

namespace PlaywrightWDE.Login
{
    public static class PerformLogin
    {
        private const int DefaultTimeout = 600_000;

        public static async Task<bool> PerformLoginAsync(
            IPage page,
            string username,
            string firstPassword,
            string secondPassword)
        {
            await page.GotoAsync(Urls.unileverPortal);

            await page.FillAsync(LoginSelectors.UsernameField, username);
            await page.FillAsync(LoginSelectors.FirstPasswordField, firstPassword);

            await page.PressAsync(LoginSelectors.FirstPasswordField, "Enter");
            Logger.Log("➡️ First login submitted");

            ILocator? secondPwd = null;

            try
            {
                await page.Locator(LoginSelectors.SecondPasswordField)
                          .WaitForAsync(new LocatorWaitForOptions { Timeout = DefaultTimeout });
                secondPwd = page.Locator(LoginSelectors.SecondPasswordField);
                Logger.Log("✅ Second password field found on main page");
            }
            catch
            {
                foreach (var frame in page.Frames)
                {
                    try
                    {
                        await frame.Locator(LoginSelectors.SecondPasswordField)
                                   .WaitForAsync(new LocatorWaitForOptions { Timeout = DefaultTimeout });
                        secondPwd = frame.Locator(LoginSelectors.SecondPasswordField);
                        Logger.Log("✅ Second password field found in a frame");
                        break;
                    }
                    catch { }
                }
            }

            if (secondPwd is null)
            {
                Logger.Log("❌ Second password field never appeared");
                await page.ScreenshotAsync(new() { Path = "login-failure.png", FullPage = true });
                return false;
            }

            await secondPwd.FillAsync(secondPassword);
            Logger.Log("✅ Second password filled");

            try
            {
                var captchaInput = page.Locator(LoginSelectors.CaptchaInput);

                if (await captchaInput.CountAsync() > 0)
                {
                    var captchaText = await page.Locator(LoginSelectors.CaptchaText).InnerTextAsync();
                    Logger.Log("🔹 Captcha detected: " + captchaText);

                    await captchaInput.FillAsync(captchaText);
                    Logger.Log("✅ Captcha filled");
                }
                else
                {
                    Logger.Log("⚠️ No captcha detected, proceeding");
                }
            }
            catch
            {
                Logger.Log("⚠️ Captcha handling failed or not present");
            }

            try
            {
                await page.ClickAsync(LoginSelectors.SecondLoginButton, new PageClickOptions
                {
                    Timeout = DefaultTimeout
                });
                Logger.Log("✅ Login submitted");
            }
            catch
            {
                Logger.Log("⚠️ Second login button not found, login may have auto-submitted");
            }

            await page.WaitForTimeoutAsync(2000);

            return true;
        }
    }
}


