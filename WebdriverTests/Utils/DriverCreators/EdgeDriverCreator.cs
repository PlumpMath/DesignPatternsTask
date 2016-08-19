using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace WebdriverTests.Utils.DriverCreators
{
    public class EdgeDriverCreator : WebDriverCreator
    {
        protected override DesiredCapabilities DesiredCapabilities => DesiredCapabilities.Edge();
        protected override CustomWebDriver WebDriver => new CustomWebDriver(new EdgeDriver());
    }
}
