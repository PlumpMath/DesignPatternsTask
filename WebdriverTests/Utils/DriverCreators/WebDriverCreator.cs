using OpenQA.Selenium.Remote;
using WebdriverTests.Helpers;

namespace WebdriverTests.Utils.DriverCreators
{
    public abstract class WebDriverCreator
    {
        public CustomWebDriver CreateDriver()
        {
            if (!ConfigFileManager.UseSeleniumGrid)
                return WebDriver;

            DesiredCapabilities.SetCapability(CapabilityType.Platform, ConfigFileManager.Platform);

            var hubUri = ConfigFileManager.GridHubUri;
            if (ConfigFileManager.UseSauceLabs)
            {
                DesiredCapabilities.SetCapability("username", ConfigFileManager.SauceLabsUsername);
                DesiredCapabilities.SetCapability("accessKey", ConfigFileManager.SauceLabsAccessKey);
                hubUri = ConfigFileManager.SauceLabsHubUri;
            }
            return new CustomWebDriver(new RemoteWebDriver(hubUri, DesiredCapabilities));
        }

        protected abstract DesiredCapabilities DesiredCapabilities { get; }
        protected abstract CustomWebDriver WebDriver { get; }
    }
}
