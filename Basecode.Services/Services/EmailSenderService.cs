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
            // when testing do not forget to yeet the information for security sake
            string userEmail = "www.faithsay@gmail.com";
            string password = "thereisnosun52";

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

        public Task SendEmailAsyncWithHtml(string receiverEmail, string subject)
        {
            // change the authentication credentials to test if it works in your device
            // when testing do not forget to yeet the information for security sake
            string userEmail = "outlook@gmail.com";
            string password = "password";

            string htmlContent = "<html><body><h1>This is an HTML email</h1><p>Hello, world!</p></body></html>";

            MailMessage message = new MailMessage();
            message.To.Add(receiverEmail);
            message.Subject = subject;
            message.Body = htmlContent;
            message.IsBodyHtml = true;
            message.From = new MailAddress(userEmail, password);


            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userEmail, password)
            };

            return client.SendMailAsync(message);
        }
    }
}
