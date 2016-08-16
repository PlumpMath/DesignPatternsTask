using System;
using NUnit.Framework;
using WebdriverTests.BusinessObjects;
using WebdriverTests.Helpers;
using WebdriverTests.Pages;

namespace WebdriverTests.Steps
{
    public class HomePageSteps : StepsBase
    {
        public void LoginAs(User user)
        {
            Console.Write($"Login as {user.Name}.. ");
            HomePage = new HomePage().OpenPage(ConfigFileManager.BaseUrl) as HomePage;
            MailPage = HomePage.Login(user.Name, user.Password);
        }

        public void VerifyThatUrlContains(string expectedPartOfUrl)
        {
            Assert.That(MailPage.Url, Does.Contain(expectedPartOfUrl), $"Page url is not contains '{expectedPartOfUrl}'");
            Console.WriteLine("OK");
        }
    }
}
