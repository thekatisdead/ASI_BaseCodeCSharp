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
        void SendEmailRejectApplication(string receiverEmail, string applicantName, string jobPosition);
        /// <summary>
        /// Sends an email to the Applicant that their Interview has been rejected
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="companyName"></param>
        /// <param name="jobPosition"></param>
        void SendEmailRejectInterview(string receiverEmail, string applicantName, string jobPosition);
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

        /// <summary>
        /// Sends a Character Reference Form to the references found in the Public application form
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="referenceName"></param>
        public void SendEmailCharacterReference(string receiverEmail, string applicantName, int applicantID, string referenceName);
        /// <summary>
        /// Sends a Response to the HR when the Character Reference Form has been Answered
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="referenceName"></param>
        /// <param name="answeredNumerator"></param>
        /// <param name="answeredDenominator"></param>
        public void SendEmailOnCharacterReferenceResponse(string receiverEmail, string applicantName, string referenceName, int answeredNumerator, int answeredDenominator);
        /// <summary>
        /// Sends a reminder to the Character Reference Form
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="referenceName"></param>
        /// <param name="jobPosition"></param>
        public void SendEmailCharacterReferenceReminder(string receiverEmail, string applicantName, string referenceName, string jobPosition);
        /// <summary>
        /// Sends an email to the HR about deciding whether or not the Applicant is credible because of the Character Reference forms
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="applicantName"></param>
        /// <param name="applicantID"></param>
        /// <param name="jobPosition"></param>
        public void SendEmailCharacterReferenceDecision(string receiverEmail, string applicantName, int applicantID, string jobPosition);
    }
}
