using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Tech_Journal_ConsoleApp
{
    public class EmailEntry
    {
        public const string Path = "c:\\temp\\appsettings.txt";
        private string _toEmailAddress;
        private string _fromEmailAddress;
        private string _emailPassword;
        
        public EmailEntry()
        {
            
        }

        public bool CheckForEmailSettings()
        {
            return File.Exists(Path);
        }

        public string GetValidEmailAddress(string email,string reason)
        {
            while (!IsValidEmailAddress(email))
            {
                Console.WriteLine($"Please enter a valid email address to {reason}");
                email = Console.ReadLine();
            }
            return email;
        }

        public void GenerateSenderEmailSettings()
        {
            using var sw = File.AppendText(Path);

            Console.WriteLine("Email Settings were not found. Creating file to store email settings.");
            _fromEmailAddress = GetValidEmailAddress(_fromEmailAddress, "send from:");
            sw.WriteLine(_fromEmailAddress);

            Console.WriteLine("Please enter the password for that email address");
            _emailPassword = Console.ReadLine();
            sw.WriteLine(_emailPassword);
        }

        public void ReadSenderEmailSettings()
        {
            using var emailSettings = new StreamReader(Path);
            _fromEmailAddress = emailSettings.ReadLine();
            _emailPassword = emailSettings.ReadLine();
        }
        
        public void SendEmail(string entry, string userName)
        {
            Console.WriteLine("Sending email");
            _toEmailAddress = GetValidEmailAddress(_toEmailAddress,"send to:");

            try
            {
                var message = new MailMessage(_fromEmailAddress, _toEmailAddress)
                {
                    Subject = $"Journal Entry for {userName},{DateTime.Now:yyyy-MM-dd}",
                    Body = @entry
                };
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_fromEmailAddress, _emailPassword),
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
