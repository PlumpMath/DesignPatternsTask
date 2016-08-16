using System;
using NUnit.Framework;

namespace WebdriverTests.Steps
{
    public class EditorMailPageSteps : StepsBase
    {
        public void HoverSendButtonAndCheckClassName(string expectedPartOfClassName)
        {
            EditorMailPage = EditorMailPage.HoverSendButton();
            Assert.That(EditorMailPage.ClassOfSendButton, Does.Contain(expectedPartOfClassName),
                "Class of send button on hover should contains " + expectedPartOfClassName);
            Console.WriteLine("OK");
        }

        public void FillMailFields(string mailTo, string subject, string message)
        {
            EditorMailPage = EditorMailPage.FillMailFields(mailTo, subject, message);
        }

        public void SaveMailToDrafts()
        {
            EditorMailPage.SaveMailToDrafts();
            Console.WriteLine("Created new mail");
        }

        public void VerifyMessageContent(string to, string subject, string message)
        {
            Assert.That(EditorMailPage.RecipientAddress, Does.Contain(to), $"Email address should be equals to '{to}'");
            Assert.That(EditorMailPage.Subject, Is.EqualTo(subject), $"Subject is not equals to '{subject}'");
            Assert.That(EditorMailPage.GetMessage(), Is.EqualTo(message), "Body of the mail should be the same as we wrote before");
            Console.WriteLine("VerifyDraftContent: OK");
        }

        public void ClickSendMessageButton()
        {
            ListMailPage = EditorMailPage.SendMail();
            EditorMailPage.RefreshPage();
        }
    }
}
