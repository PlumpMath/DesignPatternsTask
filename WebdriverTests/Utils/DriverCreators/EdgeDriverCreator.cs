using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace WebdriverTests.Utils.DriverCreators
{
    public class EdgeDriverCreator : WebDriverCreator
    {
        protected override DesiredCapabilities DesiredCapabilities => DesiredCapabilities.Edge();
        protected override IWebDriver WebDriver => new EdgeDriver();
    }
}
