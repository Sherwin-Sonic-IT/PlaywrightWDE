using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightWDE.Global.Selectors
{
    public static class LoginSelectors
    {
        public const string UsernameField = "#logonuidfield";
        public const string FirstPasswordField = "#logonpassfield";
        public const string SecondPasswordField = "input[type='password']";
        public const string FirstLoginButton = ".urBtnStdNew";
        public const string CaptchaText = "#WD3D";
        public const string CaptchaInput = "#WD44";
        public const string SecondLoginButton = "#WD4E";
    }
}


