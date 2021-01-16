using System;

namespace Tech_Journal_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            var sendEmail = new EmailEntry();
            var file = sendEmail.CheckforEmailSettings();

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
            GetWelcomeMessage(userName);
            var entry = journal.CreateJournalEntry();
            sendEmail.SendEmail(entry, userName);
        }
        public static string GetWelcomeMessage(string user)
        {
            string userName = Console.ReadLine();
            Console.WriteLine($"Hello {user}! Today's Date is {DateTime.Now}");
            return userName;
        }
    }
}
