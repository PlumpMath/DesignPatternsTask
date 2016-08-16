using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace WebdriverTests.Utils
{
    public static class WebDriverExtensions
    {
        public static void ImplicitlyWait(this IWebDriver driver, int seconds)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
        }

        private static WebDriverWait Wait(this IWebDriver driver, int seconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }

        public static void WaitUntilElementExists(this IWebDriver driver, By by, int seconds = 10)
        {
            driver.Wait(seconds).Until(ExpectedConditions.ElementExists(by));
        }

        public static void WaitUntilValueIsNotEpmty(this IWebDriver driver, By by, int seconds = 5)
        {
            driver.Wait(seconds).Until(drv => drv.FindElement(by).GetValue() != string.Empty);
        }

        public static void WaitUntilPageLoad(this IWebDriver driver, int seconds = 10)
        {
            driver.Wait(seconds).Until(drv => drv.ExecuteJavaScript<string>("return document.readyState").ToLower() == "complete");
        }

        public static void WaitUntilCountOfElementsChanged(this IWebDriver driver, By by, int initialCount, int seconds = 5)
        {
            driver.Wait(seconds).Until(drv => drv.FindElements(by).Count != initialCount);
        }

        public static void WaitForAjax(this IWebDriver driver, int seconds = 10)
        {
            driver.Wait(seconds).Until(drv => drv.ExecuteJavaScript<bool>("return jQuery.active == 0"));
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            return driver.Wait(timeoutInSeconds).Until(ExpectedConditions.ElementExists(by));
        }

        public static IList<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            return driver.Wait(timeoutInSeconds).Until(drv => drv.FindElements(by));
        }

        public static string GetValue(this IWebElement element)
        {
            return element.GetAttribute("value");
        }
    }
}
