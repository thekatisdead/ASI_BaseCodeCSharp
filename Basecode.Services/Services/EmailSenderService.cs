using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string receiverEmail, string subject, string message)
        {
            // change the authentication credentials to test if it works in your device
            string userEmail = "outlook-email@.com";
            string password = "password";

            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userEmail, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: userEmail,
                                to: receiverEmail,
                                subject,
                                message));
        }
    }
}
