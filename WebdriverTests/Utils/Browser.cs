using System;
using WebdriverTests.Helpers;
using WebdriverTests.Utils.DriverCreators;

namespace WebdriverTests.Utils
{
    public class Browser
    {
        private static readonly WebDriverCreator DriverCreator;

        private static Lazy<CustomWebDriver> lazyDriver;
        public static CustomWebDriver Driver => lazyDriver.Value;

        public static void InitDriver()
        {
            lazyDriver = new Lazy<CustomWebDriver>(DriverCreator.CreateDriver);
        }

        static Browser()
        {
            switch (ConfigFileManager.Browser)
            {
                case BrowserType.Firefox:
                    DriverCreator = new FirefoxDriverCreator();
                    break;
                case BrowserType.Chrome:
                    DriverCreator = new ChromeDriverCreator();
                    break;
                case BrowserType.InternetExplorer:
                    DriverCreator = new IEDriverCreator();
                    break;
                case BrowserType.Edge:
                    DriverCreator = new EdgeDriverCreator();
                    break;
                default:
                    throw new ArgumentException("Incorrect value of Browser");
            }
        }
    }

    public enum BrowserType
    {
        Firefox,
        Chrome,
        InternetExplorer,
        Edge
    }
}
