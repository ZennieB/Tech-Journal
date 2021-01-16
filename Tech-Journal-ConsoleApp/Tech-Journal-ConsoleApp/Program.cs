using System;

namespace Tech_Journal_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            var sendEmail = new SendEmail();
            sendEmail.ValidateEmailSettings();
            Console.WriteLine("Please enter your name:");
            var userName = GetWelcomeMessage();
            var entry = journal.JournalEntry();
            sendEmail.Send(entry, userName);
        }
        public static string GetWelcomeMessage()
        {
            string userName = Console.ReadLine();
            Console.WriteLine($"Hello {userName}! Today's Date is {DateTime.Now}");
            return userName;
        }
    }
}
