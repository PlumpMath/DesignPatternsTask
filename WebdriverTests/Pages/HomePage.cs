using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebdriverTests.Utils;

namespace WebdriverTests.Pages
{
    public class HomePage : BasePage
    {
        private const string UsernameInputName = "login";
        private const string PasswordInputName = "passwd";
        private const string SignInButtonCss = "div.domik2__submit button";

        [FindsBy(How = How.Name, Using = UsernameInputName)]
        private IWebElement usernameInput;

        [FindsBy(How = How.Name, Using = PasswordInputName)]
        private IWebElement passwordInput;

        [FindsBy(How = How.CssSelector, Using = SignInButtonCss)]
        private IWebElement signInButton;

        public ListMailPage Login(string username, string password)
        {
            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            signInButton.Click();
            Browser.Driver.WaitUntilPageLoad();
            return new ListMailPage();
        }
    }
}
