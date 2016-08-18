using System;
using System.Configuration;
using WebdriverTests.Utils;

namespace WebdriverTests.Helpers
{
    public static class ConfigFileManager
    {
        public static BrowserType Browser => (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["Browser"]);
        public static string Platform => ConfigurationManager.AppSettings["Platform"];
        public static string BaseUrl => ConfigurationManager.AppSettings["BaseUrl"];
        public static string Username => ConfigurationManager.AppSettings["Username"];
        public static string Password => ConfigurationManager.AppSettings["Password"];
        public static string SauceLabsUsername => ConfigurationManager.AppSettings["SauceLabsUsername"];
        public static string SauceLabsAccessKey => ConfigurationManager.AppSettings["SauceLabsAccessKey"];
        public static bool UseSeleniumGrid => bool.Parse(ConfigurationManager.AppSettings["UseSeleniumGrid"]);
        public static bool UseSauceLabs => bool.Parse(ConfigurationManager.AppSettings["UseSauceLabs"]);
        public static Uri GridHubUri => new Uri(ConfigurationManager.AppSettings["GridHubUrl"]);
        public static Uri SauceLabsHubUri => new Uri(ConfigurationManager.AppSettings["SauceLabsHubUrl"]);
    }
}
