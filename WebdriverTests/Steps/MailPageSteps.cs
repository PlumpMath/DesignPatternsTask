using System;

namespace WebdriverTests.Steps
{
    public class MailPageSteps : StepsBase
    {
        public void ClickComposeLink()
        {
            MailPage.RefreshPage();
            EditorMailPage = MailPage.ClickComposeLink();
        }

        public void Logoff()
        {
            Console.WriteLine("Logoff..");
            HomePage = MailPage.Logoff();
        }

        public void OpenFolder(MailFolders folder)
        {
            switch (folder)
            {
                case MailFolders.Sent:
                    ListMailPage = MailPage.OpenSentFolder();
                    break;
                case MailFolders.Drafts:
                    ListMailPage = MailPage.OpenDraftsFolder();
                    break;
                default:
                    throw new NotImplementedException($"Click on {folder} folder is not implemented");
            }
        }
    }

    public enum MailFolders
    {
        Inbox,
        Sent,
        Trash,
        Spam,
        Drafts
    }
}
