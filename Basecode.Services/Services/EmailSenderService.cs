﻿using Basecode.Services.Interfaces;
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
        string _dtEmail = "kaherbieto@outlook.up.edu.ph";
        string _senderEmail = "noreply-alliance@gmail.com";
        string _webURL = "alliancehr.azurewebsites.net";

        public void SendEmailInterviewSchedule(HrScheduler HrScheduler)
        {
            string receiverEmail, applicantName, companyName, jobPosition;
            receiverEmail = HrScheduler.EmailAddress;
            applicantName = HrScheduler.FirstName + " " + HrScheduler.LastName;
            companyName = "Alliance Software Inc.";
            jobPosition = "Programmer";

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress("Receiver Name", receiverEmail));

            email.Subject = "Interview Schedule";

            var htmlContent = File.ReadAllText("EmailTemplates/RejectApplication.html");

            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{companyName}", companyName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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
        public void SendEmailRejectApplication(string receiverEmail, string applicantName,  string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Application Rejection";

            //var path = Path.Combine("EmailTemplates", "RejectApplication.html");
            var htmlContent = File.ReadAllText("EmailTemplates/RejectApplication.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        public void SendEmailRejectInterview(string receiverEmail, string applicantName, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Interview Rejection";

            var htmlContent = File.ReadAllText("EmailTemplates/RejectInterview.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        public void SendEmailInterviewDecision(string receiverEmail, string applicantName, string companyName, string jobPosition, string typeExam)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"Interview Decision for {applicantName}";

            var htmlContent = File.ReadAllText("EmailTemplates/InterviewDecision.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{companyName}", companyName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        public void SendEmailHireConfirmation(string receiverEmail, string applicantName, int applicantID, string jobPosition, string applicantEmail)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"Hiring Confirmation for {applicantName}";

            var htmlContent = File.ReadAllText("EmailTemplates/HiringDecision.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{applicantID}", applicantID.ToString());
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{applicantEmail}", applicantEmail);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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
        public void SendEmailCharacterReferenceDecision(string receiverEmail, string applicantName, int applicantID, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"Character Reference Decision for {applicantName}";

            var htmlContent = File.ReadAllText("EmailTemplates/CharacterReferenceDecision.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{applicantID}", applicantID.ToString());
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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
        public void SendEmailCharacterReference(string receiverEmail, string applicantName, int applicantID, string referenceName, int trigger)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Character Reference Form for " + applicantName;

            var htmlContent = File.ReadAllText("EmailTemplates/CharacterReference.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{referenceName}", referenceName);
            htmlContent = htmlContent.Replace("{applicantID}", applicantID.ToString());
            htmlContent = htmlContent.Replace("{trigger}", trigger.ToString());
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        public void SendEmailCharacterReferenceGratitude(string receiverEmail, string applicantName, string jobPosition, string referenceName)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Character Reference Form for " + applicantName;

            var htmlContent = File.ReadAllText("EmailTemplates/CharacterReferenceGratitude.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{referenceName}", referenceName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        /// <summary>
        /// Sends an email Reminder to receiverEmail about the date
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="companyName"></param>
        /// <param name="jobPosition"></param>
        /// <param name="date"></param>
        public void SendEmailInterviewReminder(string receiverEmail, string applicantName, string companyName, string jobPosition, DateTime date)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Interview Reminder";

            var htmlContent = File.ReadAllText("EmailTemplates/InterviewReminder.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{companyName}", companyName);
            htmlContent = htmlContent.Replace("{interviewDate}", date.ToString());
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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

        public void SendEmailDTNotification(string applicantName, int formID, string applicantEmail, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, _dtEmail));

            email.Subject = $"{formID} Hired for {jobPosition}";

            var htmlContent = File.ReadAllText("EmailTemplates/DTNotification.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{formID}", formID.ToString());
            htmlContent = htmlContent.Replace("{applicantEmail}", applicantEmail);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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

        public void SendEmailDTDecision(string applicantName, int formID, string applicantEmail, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, _dtEmail));

            email.Subject = "Deployment Team Confirmation";

            var htmlContent = File.ReadAllText("EmailTemplates/DTDecision.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{formID}", formID.ToString());
            htmlContent = htmlContent.Replace("{applicantEmail}", applicantEmail);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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
        public void SendEmailCharacterReferenceReminder(string receiverEmail, string applicantName, string referenceName, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"Character Reference Reminder for {applicantName}";

            var htmlContent = File.ReadAllText("EmailTemplates/CharacterReferenceReminder.html");

            var button = "<a href=" + jobPosition + ">click me!</a>";

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{referenceName}", referenceName);
            htmlContent = htmlContent.Replace("{jobPosition}", button);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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

        public void SendEmailApplicantGeneration(string receiverEmail, string applicantName, int applicantID, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Application Generated";

            var htmlContent = File.ReadAllText("EmailTemplates/ApplicantGenerate.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{applicantID}", applicantID.ToString());
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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

        public void SendEmailApplicantGenerationHR(string receiverEmail, string applicantName, int applicantID, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"{applicantName} - Application Sent";

            var htmlContent = File.ReadAllText("EmailTemplates/ApplicantGenerateHR.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{applicantID}", applicantID.ToString());
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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

        public void SendEmailInterviewInstructions(string receiverEmail, string interviewName, string jobPosition, string examType,string teamsLink, string instructions, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(interviewName, receiverEmail));

            email.Subject = $"{examType} Instructions";

            var htmlContent = File.ReadAllText("EmailTemplates/InterviewInstructions.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{interviewName}", interviewName);

            htmlContent = htmlContent.Replace("{typeExam}", examType);
            htmlContent = htmlContent.Replace("{instructions}", instructions);
            htmlContent = htmlContent.Replace("{teamsLink}", teamsLink);

            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);

            htmlContent = htmlContent.Replace("{date}", date.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{startTime}", startTime.ToString("HH:mm"));
            htmlContent = htmlContent.Replace("{endTime}", endTime.ToString("HH:mm"));

            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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
        public void SendEmailInterviewInstructionsApplicant(string receiverEmail,  string applicantName, string jobPosition, string examType, string teamsLink, string instructions, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"{examType} Instructions";

            var htmlContent = File.ReadAllText("EmailTemplates/InterviewInstructionsApplicant.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);

            htmlContent = htmlContent.Replace("{typeExam}", examType);
            htmlContent = htmlContent.Replace("{instructions}", instructions);
            htmlContent = htmlContent.Replace("{teamsLink}", teamsLink);

            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);

            htmlContent = htmlContent.Replace("{date}", date.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{startTime}", startTime.ToString("HH:mm"));
            htmlContent = htmlContent.Replace("{endTime}", endTime.ToString("HH:mm"));

            htmlContent = htmlContent.Replace("{webURL}", _webURL);

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

        public void SendEmailInterviewGeneration(string receiverEmail, string interviewName, string applicantName, int applicantID, string jobPosition, string typeExam, int interviewId,DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(interviewName, receiverEmail));

            email.Subject = $"{typeExam} Generated";

   
            var htmlContent = File.ReadAllText("EmailTemplates/InterviewGenerate.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{interviewName}", interviewName);
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{applicantID}", applicantID.ToString());
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{typeExam}", typeExam);
            htmlContent = htmlContent.Replace("{interviewId}", interviewId.ToString());
            htmlContent = htmlContent.Replace("{date}", date.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{startTime}", startTime.ToString("HH:mm"));
            htmlContent = htmlContent.Replace("{endTime}", endTime.ToString("HH:mm"));
            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        public void SendEmailInterviewGenerationApplicant(string receiverEmail, string applicantName,  string jobPosition, string typeExam, int interviewId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"{typeExam} Generated";


            var htmlContent = File.ReadAllText("EmailTemplates/InterviewGenerate.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{typeExam}", typeExam);
            htmlContent = htmlContent.Replace("{interviewId}", interviewId.ToString());
            htmlContent = htmlContent.Replace("{date}", date.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{startTime}", startTime.ToString("HH:mm"));
            htmlContent = htmlContent.Replace("{endTime}", endTime.ToString("HH:mm"));

            htmlContent = htmlContent.Replace("{webURL}", _webURL);

            //var button = $" <input type = 'datetime-local' min = '{startTime.ToString(""yyyy-MM-dd"")}' max = '{endTime.ToString(""yyyy-MM-dd"")}'>";
            //htmlContent = htmlContent.Replace("{button}",button);
            //htmlContent = htmlContent.Replace("{startTime}","2023-07-28");
            //htmlContent = htmlContent.Replace("{endTime}", "2023-07-29");


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

        // Approval Email in Application and Screening
        public void SendEmailHRApplicationDecision(string receiverEmail, int applicantId ,string applicantName, string jobPosition)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = $"Application Decision for {applicantName}";

            var htmlContent = File.ReadAllText("EmailTemplates/HRApplicationDecision.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{jobPosition}", jobPosition);
            htmlContent = htmlContent.Replace("{applicantId}", applicantId.ToString());

            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Application Update";

            var htmlContent = File.ReadAllText("EmailTemplates/OnUpdate.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{previousStatus}", previousStatus);
            htmlContent = htmlContent.Replace("{newStatus}", newStatus);

            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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

        public void SendEmailOnCharacterReferenceResponse(string receiverEmail, string applicantName, string referenceName, int answeredNumerator, int answeredDenominator)
        {
            var email = new MimeMessage();

            // If you wanna test for email functionalities, change the Sender Name to your email
            // and the receiver name to your other email.
            // To perform it properly, follow the link below
            // https://mailtrap.io/blog/csharp-send-email-gmail/

            email.From.Add(new MailboxAddress("HR Automated Tracking", _senderEmail));
            email.To.Add(new MailboxAddress(applicantName, receiverEmail));

            email.Subject = "Character Reference Responded For " + applicantName;

            var htmlContent = File.ReadAllText("EmailTemplates/CharacterReferenceResponded.html");

            // this is to replace the placeholders
            htmlContent = htmlContent.Replace("{applicantName}", applicantName);
            htmlContent = htmlContent.Replace("{referenceName}", referenceName);
            htmlContent = htmlContent.Replace("{answeredNumerator}", answeredNumerator.ToString());
            htmlContent = htmlContent.Replace("{answeredDenominator}", answeredDenominator.ToString());

            htmlContent = htmlContent.Replace("{webURL}", _webURL);


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
    }
}
