using System;

namespace Tech_Journal_ConsoleApp
{
    public class Journal
    {
        public string CreateJournalEntry()
        {
            Console.WriteLine("Please enter today's journal entry:");
            var entry = Console.ReadLine();
            return entry;
        }
    }
}
