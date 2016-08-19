using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebdriverTests.Utils;

namespace WebdriverTests.Pages
{
    public class EditorMailPage : MailPage
    {
        private const string ToInputCss = "div[class*='input_to'] input[type='text']";
        private const string SubjectInputId = "compose-subj";
        private const string EditorFrameTag = "iframe";
        private const string MessageTextAreaId = "tinymce";
        private const string DraftSavedNoticeCss = "span[class*='helper__date'] span";
        private const string SendButtonsCss = "button[class*=\"send-button\"]";

        [FindsBy(How = How.CssSelector, Using = ToInputCss)]
        private IWebElement toInput;

        [FindsBy(How = How.Id, Using = SubjectInputId)]
        private IWebElement subjectInput;

        [FindsBy(How = How.TagName, Using = EditorFrameTag)]
        private IWebElement editorFrame;

        [FindsBy(How = How.Id, Using = MessageTextAreaId)]
        private IWebElement messageTextArea;

        [FindsBy(How = How.CssSelector, Using = SendButtonsCss)]
        private IList<IWebElement> sendButtons;

        public string RecipientAddress => toInput.GetAttribute("value");
        public string Subject => subjectInput.GetAttribute("value");
        public string ClassOfSendButton => sendButtons.First().GetAttribute("class");

        public EditorMailPage FillMailFields(string address, string subject, string message)
        {
            Browser.Driver.WaitForAjax();
            Browser.Driver.WaitUntilElementExists(By.CssSelector(ToInputCss));
            toInput.SendKeys(address);
            subjectInput.SendKeys(subject);
            Browser.Driver.SwitchTo().Frame(editorFrame);
            Browser.Driver.ImplicitlyWait(10);
            messageTextArea.SendKeys(message);
            Browser.Driver.SwitchTo().DefaultContent();
            return this;
        }

        public void SaveMailToDrafts()
        {
            Browser.Driver.WaitUntilElementExists(By.CssSelector(DraftSavedNoticeCss), 15);
        }

        public string GetMessage()
        {
            Browser.Driver.SwitchTo().Frame(editorFrame);
            string message = messageTextArea.Text;
            Browser.Driver.SwitchTo().DefaultContent();
            return message;
        }

        public ListMailPage SendMail()
        {
            sendButtons.First().Click();
            Browser.Driver.WaitForAjax();
            return new ListMailPage();
        }

        public EditorMailPage HoverSendButton()
        {
            Browser.Driver.WaitUntilElementExists(By.CssSelector(SendButtonsCss));
            Browser.Driver.ExecuteJavaScript<object>("$(arguments[0]).mouseenter()", sendButtons.First());
            Browser.Driver.ImplicitlyWait(3);
            return this;
        }
    }
}
