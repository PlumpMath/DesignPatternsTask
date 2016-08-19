using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using WebdriverTests.BusinessObjects;
using WebdriverTests.Helpers;
using WebdriverTests.Steps;
using WebdriverTests.Utils;

namespace WebdriverTests.Tests
{
    [TestFixture]
    [Author("Andrei Arekhva")]
    public abstract class TestBase
    {
        private const string MailServiceUrl = "mail.yandex.by";

        protected HomePageSteps HomePage;
        protected MailPageSteps MailPage;
        protected ListMailPageSteps ListMailPage;
        protected EditorMailPageSteps EditorMailPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Browser.InitDriver();

            HomePage = new HomePageSteps();
            MailPage = new MailPageSteps();
            ListMailPage = new ListMailPageSteps();
            EditorMailPage = new EditorMailPageSteps();

            var user = new User(ConfigFileManager.Username, ConfigFileManager.Password);
            HomePage.LoginAs(user);
            HomePage.VerifyThatUrlContains(MailServiceUrl);
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                Browser.Driver.TakeScreenshot(TestContext.CurrentContext.Test.Name);

            MailPage.Logoff();
            Console.WriteLine("Close Browser..");
            Browser.Driver.Manage().Cookies.DeleteAllCookies();
            Browser.Driver.Quit();
        }
    }
}
