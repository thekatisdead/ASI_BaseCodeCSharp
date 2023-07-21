using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IEmailSenderService
    {
        void SendEmailInterviewSchedule(HrScheduler HrScheduler);
        /// <summary>
        /// Sends an email to the Applicant that their Appliication has been rejected
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="companyName"></param>
        /// <param name="jobPosition"></param>
        void SendEmailRejectApplication(string receiverEmail, string applicantName,string companyName, string jobPosition);
        /// <summary>
        /// Sends an email to the Applicant that their Interview has been rejected
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="companyName"></param>
        /// <param name="jobPosition"></param>
        void SendEmailRejectInterview(string receiverEmail, string applicantName, string companyName, string jobPosition);
        /// <summary>
        /// Sends an Email to the Interviewer after the Interview about their decision
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="companyName"></param>
        /// <param name="jobPosition"></param>
        void SendEmailInterviewDecision(string receiverEmail, string applicantName, string companyName, string jobPosition);
        /// <summary>
        /// Sends an Email to remind the Users about an upcoming Interview
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="companyName"></param>
        /// <param name="jobPosition"></param>
        /// <param name="date"></param>
        void SendEmailInterviewReminder(string receiverEmail, string applicantName, string companyName, string jobPosition, DateTime date);
        /// <summary>
        /// Sends an Email to the HR about the update to the applicant's status
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="previousStatus"></param>
        /// <param name="newStatus"></param>
        void SendEmailOnUpdateApplicantStatus(string receiverEmail, string applicantName, string previousStatus, string newStatus);
        /// <summary>
        /// Sends an Email to the HR when the application is Shortlisted to decide whether or not the Applicant can proceed to the Interview
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantId"></param>
        /// <param name="applicantName"></param>
        /// <param name="jobPosition"></param>
        void SendEmailHRApplicationDecision(string receiverEmail, int applicantId,string applicantName, string jobPosition);
    }
}
