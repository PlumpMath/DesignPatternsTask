using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace WebdriverTests.Utils.DriverCreators
{
    public class IEDriverCreator : WebDriverCreator
    {
        protected override DesiredCapabilities DesiredCapabilities => DesiredCapabilities.InternetExplorer();
        protected override CustomWebDriver WebDriver => new CustomWebDriver(new InternetExplorerDriver());
    }
}
