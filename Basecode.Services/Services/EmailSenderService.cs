using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace Basecode.Services.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string receiverEmail, string subject, string message)
        {
            /*
            // change the authentication credentials to test if it works in your device
            // when testing do not forget to yeet the information for security sake
            string userEmail = "sender-email@gmail.com";
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
            */
            return null;
        }

        public Task SendEmailAsyncWithHtml(string receiverEmail, string subject)
        {
            /*
            // change the authentication credentials to test if it works in your device
            // when testing do not forget to yeet the information for security sake
            string userEmail = "sender-email@gmail.com";
            string password = "password";

            string htmlContent = "<html><body><h1>This is an HTML email</h1><p>Hello, world!</p><form asp-controller=\"Home\" asp-action=\"Index\"><button type=\"submit\">Click Me!</button></form></body></html>";
            
            // This line of code yeets to the views, though it does not render the views into html, v sus too
            //string htmlContent = File.ReadAllText("Views/Home/Index.cshtml");

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

            return client.SendMailAsync(message);*/
            return null;
        }

        public void SendEmailRejectApplication(string receiverEmail, string companyName, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("Sender Name", "kermherbieto52@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", "kaherbieto@outlook.up.edu.ph"));

            email.Subject = "Application Rejection";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $@"<b>Application Rejection</b><p>We are sorry to say that your Application to {companyName} as {jobPosition} has been rejected</p>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                // Also: This is PM's email kek, rip bozo ig.
                smtp.Authenticate("kermherbieto52@gmail.com", "sfltmfkdvdiuhabi");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
            /*
            // change the authentication credentials to test if it works in your device
            // when testing do not forget to yeet the information for security sake
            string userEmail = "sender-email@gmail.com";
            string password = "password";

            // html needs to be designed but this is what it looks like
            string htmlContent = $@"<html><body><h1>Application Rejection</h1><p>Sorrie sob, your application to be the {jobPosition} at {companyName} has been yeeted</p></body></html>";

            // This line of code yeets to the views, though it does not render the views into html, v sus too
            //string htmlContent = File.ReadAllText("Views/Home/Index.cshtml");

            MailMessage message = new MailMessage();
            message.To.Add(receiverEmail);
            message.Subject = "Application Rejection";
            message.Body = htmlContent;
            message.IsBodyHtml = true;
            message.From = new MailAddress(userEmail, password);


            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userEmail, password)
            };

            return client.SendMailAsync(message);*/
        }
        public Task SendEmailOnUpdateApplicantStatus(string receiverEmail, string applicantName, string previousStatus, string newStatus)
        {
            /*
            // change the authentication credentials to test if it works in your device
            // when testing do not forget to yeet the information for security sake
            string userEmail = "kermherbieto52@gmail.com";
            string password = "sfltmfkdvdiuhabi";

            // html needs to be designed but this is what it looks like
            string htmlContent = $@"<html><body><h1>Application Notification</h1><p>This is to inform you that {applicantName}'s status has been recently changed from {previousStatus} to {newStatus}</p></body></html>";

            // This line of code yeets to the views, though it does not render the views into html, v sus too
            //string htmlContent = File.ReadAllText("Views/Home/Index.cshtml");

            MailMessage message = new MailMessage();
            message.To.Add(receiverEmail);
            message.Subject = $@"Applicant Update - {applicantName}";
            message.Body = htmlContent;
            message.IsBodyHtml = true;
            message.From = new MailAddress(userEmail, password);


            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userEmail, password)
            };

            return client.SendMailAsync(message);*/
            return null;
        }
    }
}
