
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightWDE.Global.Navigation
{
    public sealed class NavNode
    {
        public string Key { get; }
        public string Display { get; }

        public NavNode(string key, string display)
        {
            Key = key;
            Display = display;
        }

        public override string ToString() => Display;
    }
    
}