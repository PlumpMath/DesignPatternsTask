using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace WebdriverTests.Utils.DriverCreators
{
    public class ChromeDriverCreator : WebDriverCreator
    {
        protected override DesiredCapabilities DesiredCapabilities => DesiredCapabilities.Chrome();
        protected override CustomWebDriver WebDriver => new CustomWebDriver(new ChromeDriver());
    }
}
