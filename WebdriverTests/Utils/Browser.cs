using System;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using WebdriverTests.Helpers;

namespace WebdriverTests.Utils
{
    public static class Browser
    {
        private static Lazy<IWebDriver> lazyDriver;
        public static IWebDriver Driver => lazyDriver.Value;

        public static void InitDriver()
        {
            lazyDriver = new Lazy<IWebDriver>(SetUpBrowser);
        }

        private static IWebDriver SetUpBrowser()
        {
            if (ConfigFileManager.UseSeleniumGrid)
                return SetupRemoteWebdriver();

            switch (ConfigFileManager.Browser)
            {
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.InternetExplorer:
                    return new InternetExplorerDriver();
                case BrowserType.Edge:
                    return new EdgeDriver();
                default:
                    throw new ArgumentException("Incorrect value of WebDriver");
            }
        }

        private static RemoteWebDriver SetupRemoteWebdriver()
        {
            DesiredCapabilities caps;
            switch (ConfigFileManager.Browser)
            {
                case BrowserType.Firefox:
                    caps = DesiredCapabilities.Firefox();
                    break;
                case BrowserType.Chrome:
                    caps = DesiredCapabilities.Chrome();
                    break;
                case BrowserType.InternetExplorer:
                    caps = DesiredCapabilities.InternetExplorer();
                    break;
                case BrowserType.Edge:
                    caps = DesiredCapabilities.Edge();
                    break;
                default:
                    throw new ArgumentException("Incorrect value of WebDriver");
            }
            caps.SetCapability(CapabilityType.Platform, ConfigFileManager.Platform);

            if (ConfigFileManager.UseSauceLabs)
            {
                caps.SetCapability("username", ConfigFileManager.SauceLabsUsername);
                caps.SetCapability("accessKey", ConfigFileManager.SauceLabsAccessKey);
                return new RemoteWebDriver(ConfigFileManager.SauceLabsHubUrl, caps);
            }
            return new RemoteWebDriver(ConfigFileManager.GridHubUri, caps);
        }

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
