using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    public class Journal
    {
        public string CreateJournalEntry()
        {
            Console.WriteLine("\nPlease enter today's journal entry:");
            string entry = Console.ReadLine();
            return entry;
        }
        
    }
}
