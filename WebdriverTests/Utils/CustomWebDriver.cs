using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace WebdriverTests.Utils
{
    public class CustomWebDriver
    {
        private readonly IWebDriver driver;
        private readonly string screenshotsDirectory;

        public Actions Actions => new Actions(driver);
        public string PageSource => driver.PageSource;
        public string Title => driver.Title;

        public string Url
        {
            get { return driver.Url; }
            set { driver.Url = value; }
        }

        public CustomWebDriver(IWebDriver driver)
        {
            this.driver = driver;

            screenshotsDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Screenshots\\";
            Directory.CreateDirectory(screenshotsDirectory);
        }

        public IWebElement FindElement(By by)
        {
            ImplicitlyWait(5);
            return driver.FindElement(by);
        }

        public IList<IWebElement> FindElements(By by)
        {
            ImplicitlyWait(5);
            return driver.FindElements(by);
        }

        public IOptions Manage()
        {
            return driver.Manage();
        }

        public INavigation Navigate()
        {
            return driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            ImplicitlyWait(5);
            return driver.SwitchTo();
        }

        public void TakeScreenshot(string name)
        {
            driver.TakeScreenshot().SaveAsFile($"{screenshotsDirectory}{name}.png", ImageFormat.Png);
        }

        public T ExecuteJavaScript<T>(string script, params object[] args)
        {
            return driver.ExecuteJavaScript<T>(script, args);
        }

        public void InitElements(object page)
        {
            PageFactory.InitElements(driver, page);
        }

        public void Close()
        {
            driver.Close();
        }

        public void Dispose()
        {
            driver.Dispose();
        }

        public void Quit()
        {
            driver.Quit();
        }

        #region waiters

        public void ImplicitlyWait(int seconds)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
        }

        private WebDriverWait Wait(int seconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }

        public void WaitUntilElementExists(By by, int seconds = 10)
        {
            Wait(seconds).Until(ExpectedConditions.ElementExists(by));
        }

        public void WaitUntilValueIsNotEpmty(By by, int seconds = 5)
        {
            Wait(seconds).Until(drv => drv.FindElement(by).GetAttribute("value") != string.Empty);
        }

        public void WaitUntilPageLoad(int seconds = 10)
        {
            Wait(seconds).Until(drv => drv.ExecuteJavaScript<string>("return document.readyState").ToLower() == "complete");
        }

        public void WaitUntilCountOfElementsChanged(By by, int initialCount, int seconds = 5)
        {
            Wait(seconds).Until(drv => drv.FindElements(by).Count != initialCount);
        }

        public void WaitForAjax(int seconds = 10)
        {
            Wait(seconds).Until(drv => drv.ExecuteJavaScript<bool>("return jQuery.active == 0"));
        }

        public IWebElement FindElement(By by, int timeoutInSeconds)
        {
            return Wait(timeoutInSeconds).Until(ExpectedConditions.ElementExists(by));
        }

        public IList<IWebElement> FindElements(By by, int timeoutInSeconds)
        {
            return Wait(timeoutInSeconds).Until(drv => drv.FindElements(by));
        }

        #endregion
    }
}
