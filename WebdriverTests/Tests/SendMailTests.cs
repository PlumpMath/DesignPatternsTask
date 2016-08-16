using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using WebdriverTests.Steps;

namespace WebdriverTests.Tests
{
    public class SendMailTests : TestBase
    {
        private const string MailTo = "webdrivertest@yandex.by";
        private const string Message = "Body of test message";
        private string subject;
        private const int CountOfTests = 3;

        [Test]
        [Repeat(CountOfTests)]
        public void MailServiceCorrectWork()
        {
            // Creating new message
            subject = "Test mail, " + DateTime.Now;
            MailPage.ClickComposeLink();
            EditorMailPage.FillMailFields(MailTo, subject, Message);

            // Saving to Drafts folder
            EditorMailPage.SaveMailToDrafts();
            MailPage.OpenFolder(MailFolders.Drafts);
            ListMailPage.VerifyExistenceOfTheMailInFolder(MailTo, subject, true);

            // Verifying content of the draft message
            ListMailPage.OpenMessage(MailTo, subject);
            EditorMailPage.VerifyMessageContent(MailTo, subject, Message);

            // Sending message and verifying its moving from Drafts to Sent folder
            EditorMailPage.ClickSendMessageButton();
            MailPage.OpenFolder(MailFolders.Drafts);
            ListMailPage.VerifyExistenceOfTheMailInFolder(MailTo, subject, false);
            MailPage.OpenFolder(MailFolders.Sent);
            ListMailPage.VerifyExistenceOfTheMailInFolder(MailTo, subject, true);
            Console.WriteLine("VerifyMailSent: OK");
        }

        public override void OneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                MailPage.OpenFolder(MailFolders.Sent);
                ListMailPage.DeleteMessages(CountOfTests);
            }
            base.OneTimeTearDown();
        }
    }
}
