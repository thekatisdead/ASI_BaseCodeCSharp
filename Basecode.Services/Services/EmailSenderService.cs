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
        }
        public void SendEmailOnUpdateApplicantStatus(string receiverEmail, string applicantName, string previousStatus, string newStatus)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("Sender Name", "kermherbieto52@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", "kaherbieto@outlook.up.edu.ph"));

            email.Subject = "Application Update";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $@"<b>Application Name</b><p>This is to inform you that {applicantName}'s status has been changed from {previousStatus} to {newStatus}</p>"
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
        }
    }
}
