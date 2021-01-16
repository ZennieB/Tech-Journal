using System;

namespace Tech_Journal_ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var journal = new Journal();
            var sendEmail = new EmailEntry();
            var file = sendEmail.CheckForEmailSettings();
            if (!file)
            {
                sendEmail.GenerateSenderEmailSettings();
            }
            else
            {
                sendEmail.ReadSenderEmailSettings();
            }
            Console.WriteLine("Please enter your name:");
            var userName = Console.ReadLine();
            Console.WriteLine($"Hello {userName}! Today's Date is {DateTime.Now}");
            var entry = journal.CreateJournalEntry();
            sendEmail.SendEmail(entry, userName);
        }
    }
}
