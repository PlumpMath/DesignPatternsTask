using System;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WebdriverTests.Helpers;
using WebdriverTests.Utils.DriverCreators;

namespace WebdriverTests.Utils
{
    public class Browser
    {
        private static readonly WebDriverCreator DriverCreator;

        private static Lazy<IWebDriver> lazyDriver;
        public static IWebDriver Driver => lazyDriver.Value;

        public static void InitDriver()
        {
            lazyDriver = new Lazy<IWebDriver>(DriverCreator.CreateDriver);
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

        // todo: move to decorator
        public static void TakeScreenshot(string name)
        {
            string screenshotsDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Screenshots\\";
            Directory.CreateDirectory(screenshotsDirectory);
            Driver.TakeScreenshot().SaveAsFile($"{screenshotsDirectory}{name}.png", ImageFormat.Png);
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
