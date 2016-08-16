using System;
using NUnit.Framework;

namespace WebdriverTests.Tests
{
    public class HoverButtonTests : TestBase
    {
        private const string ExpectedClassName = "ui-state-hover";

        [Test]
        public void CheckSendButtonHoverClass()
        {
            Console.Write("Checking class of send button on hover.. ");
            MailPage.ClickComposeLink();
            EditorMailPage.HoverSendButtonAndCheckClassName(ExpectedClassName);
        }
    }
}
