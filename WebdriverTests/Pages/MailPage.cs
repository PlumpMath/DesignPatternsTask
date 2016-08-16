using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebdriverTests.Utils;

namespace WebdriverTests.Pages
{
    public abstract class MailPage : BasePage
    {
        private const string ComposeLinkCss = "a[href*='compose']";
        private const string DeleteLinkCss = "a[data-action='delete']";
        private const string FoldersLinksCss = "div.b-folders__i a[href='#{0}']";
        private const string HeaderUserLinkId = "nb-1";
        private const string DropdownMenuLogoutLinkCss = "div#user-dropdown-popup a[href*='action=logout']";

        [FindsBy(How = How.CssSelector, Using = ComposeLinkCss)]
        private IWebElement composeLink;

        [FindsBy(How = How.CssSelector, Using = DeleteLinkCss)]
        protected IWebElement DeleteLink;

        [FindsBy(How = How.Id, Using = HeaderUserLinkId)]
        private IWebElement headerUserLink;

        [FindsBy(How = How.CssSelector, Using = DropdownMenuLogoutLinkCss)]
        private IWebElement menuLogoutLink;

        protected IWebElement InboxLink => Browser.Driver.FindElement(By.CssSelector(string.Format(FoldersLinksCss, "inbox")));
        protected IWebElement SentLink => Browser.Driver.FindElement(By.CssSelector(string.Format(FoldersLinksCss, "sent")));
        protected IWebElement TrashLink => Browser.Driver.FindElement(By.CssSelector(string.Format(FoldersLinksCss, "trash")));
        protected IWebElement SpamLink => Browser.Driver.FindElement(By.CssSelector(string.Format(FoldersLinksCss, "spam")));
        protected IWebElement DraftsLink => Browser.Driver.FindElement(By.CssSelector(string.Format(FoldersLinksCss, "draft")));

        public EditorMailPage ClickComposeLink()
        {
            Browser.Driver.WaitUntilElementExists(By.CssSelector(ComposeLinkCss));
            composeLink.Click();
            Browser.Driver.WaitUntilPageLoad();
            return new EditorMailPage();
        }

        public ListMailPage OpenSentFolder()
        {
            Browser.Driver.ImplicitlyWait(3);
            SentLink.Click();
            Browser.Driver.WaitForAjax(15);
            return new ListMailPage();
        }

        public ListMailPage OpenDraftsFolder()
        {
            DraftsLink.Click();
            Browser.Driver.WaitForAjax();
            return new ListMailPage();
        }

        public HomePage Logoff()
        {
            Browser.Driver.WaitForAjax();
            headerUserLink.Click();
            menuLogoutLink.Click();
            Browser.Driver.WaitUntilPageLoad();
            return new HomePage();
        }
    }
}
