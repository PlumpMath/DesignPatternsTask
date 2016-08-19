using WebdriverTests.Utils;

namespace WebdriverTests.Pages
{
    public abstract class BasePage
    {
        public string Url => Browser.Driver.Url;

        protected BasePage()
        {
            Browser.Driver.InitElements(this);
        }

        public BasePage OpenPage(string url)
        {
            Browser.Driver.Navigate().GoToUrl(url);
            Browser.Driver.WaitUntilPageLoad();
            return this;
        }

        public void RefreshPage()
        {
            //Browser.Driver.Navigate().Refresh();
            Browser.Driver.ExecuteJavaScript<object>("location.reload();");
            Browser.Driver.WaitUntilPageLoad();
        }
    }
}
