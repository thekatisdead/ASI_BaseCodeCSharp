using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Basecode.Data.Models;

namespace Basecode.Services.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public void SendEmailInterviewSchedule(HrScheduler HrScheduler)
        {
            string receiverEmail, applicantName, companyName, jobPosition;
            receiverEmail = HrScheduler.EmailAddress;
            applicantName = HrScheduler.FirstName + " " + HrScheduler.LastName;
            companyName = "Alliance Software Inc.";
            jobPosition = "Programmer";

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Sender Name", "kermherbieto52@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", receiverEmail));

            email.Subject = "Interview Schedule";

            var htmlContent = File.ReadAllText("EmailTemplates/RejectApplication.html");

            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{companyName}", companyName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlContent
            };

            using (var smtp = new SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect("smtp.gmail.com", 587, false);

                smtp.Authenticate("kermherbieto52@gmail.com", "sfltmfkdvdiuhabi");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        public void SendEmailRejectApplication(string receiverEmail, string applicantName, string companyName, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("Sender Name", "kermherbieto52@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", "kaherbieto@outlook.up.edu.ph"));

            email.Subject = "Application Rejection";

            var htmlContent = File.ReadAllText("EmailTemplates/RejectApplication.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{companyName}", companyName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);


            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlContent
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
        public void SendEmailHRApplicationDecision(string receiverEmail, int applicantId ,string applicantName, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("Sender Name", "kermherbieto52@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", "kaherbieto@outlook.up.edu.ph"));

            email.Subject = "Application Rejection";

            var htmlContent = File.ReadAllText("EmailTemplates/HRApplicationDecision.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            //htmlContent = htmlContent.Replace("{companyName}", companyName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);


            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlContent
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

            var htmlContent = File.ReadAllText("EmailTemplates/OnUpdate.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{previousStatus}", previousStatus);
            htmlContent = htmlContent.Replace("{newStatus}", newStatus); 


            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text=htmlContent
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
