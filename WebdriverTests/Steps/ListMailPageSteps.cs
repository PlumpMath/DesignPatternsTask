using NUnit.Framework;
using System;
using WebdriverTests.Pages;

namespace WebdriverTests.Steps
{
    public class ListMailPageSteps : StepsBase
    {
        public void VerifyExistenceOfTheMailInFolder(string to, string subject, bool shouldExists)
        {
            Assert.That(ListMailPage.IsMailPresentInFolder(to, subject) == shouldExists,
                shouldExists ? "The mail is not present in folder" : "The mail is present in folder");
        }

        public void OpenMessage(string mailTo, string subject)
        {
            EditorMailPage = ListMailPage.OpenMessage(mailTo, subject);
        }

        public void DeleteMessages(int count)
        {
            int countOfMessages = ListMailPage.CountOfMessagesInFolder;
            ListMailPage = ListMailPage.SelectMessagesUsingMousePlusShift(0, count)
                                       .DragAndDropMessage(count / 2, DroppableElements.DeleteButton);
            Assert.That(ListMailPage.CountOfMessagesInFolder, Is.EqualTo(countOfMessages - count), "Messages weren't deleted");
            Console.WriteLine("DeleteLastSentMessages: OK");
        }
    }
}
