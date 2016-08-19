using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebdriverTests.Utils;

namespace WebdriverTests.Pages
{
    public class ListMailPage : MailPage
    {
        private const string ListOfMessagesCss = ".block-messages:not([style]) [class*='message-id']";
        private const string ListOfCheckboxesCss = ".block-messages:not([style]) .block-messages-wrap input";

        [FindsBy(How = How.CssSelector, Using = ListOfMessagesCss)]
        private IList<IWebElement> messages;

        [FindsBy(How = How.CssSelector, Using = ListOfCheckboxesCss)]
        private IList<IWebElement> checkboxes;

        public int CountOfMessagesInFolder => messages.Count;

        public bool IsMailPresentInFolder(string to, string subject)
        {
            Browser.Driver.WaitForAjax();
            return GetMessageByAddressAndSubject(to, subject) != null;
        }

        public EditorMailPage OpenMessage(string to, string subject)
        {
            GetMessageByAddressAndSubject(to, subject).Click();
            Browser.Driver.WaitForAjax();
            return new EditorMailPage();
        }

        public ListMailPage SelectMessagesUsingMousePlusShift(int from, int count)
        {
            Browser.Driver.Actions.Click(checkboxes[from]).Perform();

            if (count > 1)
                Browser.Driver.Actions
                    .KeyDown(Keys.Shift)
                    .Click(checkboxes[from + count - 1])
                    .KeyUp(Keys.Shift).Perform();

            return this;
        }

        public ListMailPage DragAndDropMessage(int index, DroppableElements element)
        {
            IWebElement droppableElement;
            switch (element)
            {
                case DroppableElements.InboxFolder:
                    droppableElement = InboxLink;
                    break;
                case DroppableElements.SentFolder:
                    droppableElement = SentLink;
                    break;
                case DroppableElements.TrashFolder:
                    droppableElement = TrashLink;
                    break;
                case DroppableElements.SpamFolder:
                    droppableElement = SpamLink;
                    break;
                case DroppableElements.DraftsFolder:
                    droppableElement = DraftsLink;
                    break;
                case DroppableElements.DeleteButton:
                    droppableElement = DeleteLink;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, "Wrong droppable element");
            }

            Browser.Driver.Actions
                .ClickAndHold(messages[index])
                .MoveByOffset(-50, 0).MoveByOffset(0, 50)
                .MoveToElement(droppableElement).Click()
                .Release().Build().Perform();

            Browser.Driver.WaitForAjax();
            RefreshPage();
            return this;
        }

        private IWebElement GetMessageByAddressAndSubject(string to, string subject)
        {
            Console.WriteLine($"In {Url.Substring(Url.IndexOf('#'))} {messages.Count} messages, {checkboxes.Count} checkboxes");
            return messages.FirstOrDefault(d => d.Text.Contains(to) && d.Text.Contains(subject));
        }
    }

    public enum DroppableElements
    {
        InboxFolder,
        SentFolder,
        TrashFolder,
        SpamFolder,
        DraftsFolder,
        DeleteButton
    }
}
