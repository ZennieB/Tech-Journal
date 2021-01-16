using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    class EmailEntry
    {
        public const string Path = "c:\\temp\\appsettings.txt";

        private bool _validEmail;
        private string _toThisEmailAddress;
        private string _emailUsername;
        private string _emailPassword;


        public EmailEntry()
        {
            _validEmail = false;
        }

        public bool CheckforEmailSettings()
        {
            if (!File.Exists(Path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void GenerateSenderEmailSettings()
        {
            using (var sw = File.AppendText(Path))
            {
                Console.WriteLine("Email Settings were not found. Creating file to store email settings.");
                while (!_validEmail)
                {
                    Console.WriteLine("Please enter an email address to send the Journal entry from");
                    _emailUsername = Console.ReadLine();
                    _validEmail = IsValidEmailAddress(_emailUsername);
                }
                sw.WriteLine(_emailUsername);
                Console.WriteLine("Please enter the password for that email address");
                _emailPassword = Console.ReadLine();
                sw.WriteLine(_emailPassword);
                _validEmail = false;
            }
        }

        public void ReadSenderEmailSettings()
        {
            using (var emailSettings = new StreamReader(Path))
            {
                _emailUsername = emailSettings.ReadLine();
                _emailPassword = emailSettings.ReadLine();
            }
        }
        
        public void SendEmail(string entry, string userName)
        {

            while (!_validEmail)
            {
                Console.WriteLine("Please enter a valid email address to send the journal entry:");
                _toThisEmailAddress = Console.ReadLine();
                _validEmail = IsValidEmailAddress(_toThisEmailAddress);
            }

            try
            {
                var message = new MailMessage(_emailUsername, _toThisEmailAddress)
                {
                    Subject = $"Journal Entry for {userName},{DateTime.Now:yyyy-MM-dd}",
                    Body = @entry
                };
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_emailUsername, _emailPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                File.Delete(Path);
            }

        }
        public static bool IsValidEmailAddress(string address) => address != null && new EmailAddressAttribute().IsValid(address);

    }
}
