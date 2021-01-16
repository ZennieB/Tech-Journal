using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    class SendEmail
    {
        public const string Path = "c:\\temp\\appsettings.txt";

        private bool _validEmail;
        private string _toThisEmailAddress;
        private string _emailUsername;
        private string _emailPassword;


        public SendEmail()
        {
            _validEmail = false;
        }
        public void ValidateEmailSettings()
        {
            if (!File.Exists(Path))
            {
                using (var sw = File.AppendText(Path))
                {
                    Console.WriteLine("\nEmail Settings were not found. Creating file to store email settings.");
                    while (!_validEmail)
                    {
                        Console.WriteLine("\nPlease enter an email address to send the Journal entry from");
                        _emailUsername = Console.ReadLine();
                        _validEmail = IsValidEmailAddress(_emailUsername);
                    }
                    sw.WriteLine(_emailUsername);
                    Console.WriteLine("\nPlease enter the password for that email address");
                    _emailPassword = Console.ReadLine();
                    sw.WriteLine(_emailPassword);
                    _validEmail = false;
                }
            }
            else
            {
                using (var ab = new StreamReader(Path))
                {
                    _emailUsername = ab.ReadLine();
                    _emailPassword = ab.ReadLine();
                }
            }
        }
        public void Send(string entry, string userName)
        {

            while (!_validEmail)
            {
                Console.WriteLine("\nPlease enter a valid email address to send the journal entry:");
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
