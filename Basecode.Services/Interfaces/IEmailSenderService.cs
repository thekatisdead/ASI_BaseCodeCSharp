using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IEmailSenderService
    {

        void SendEmailRejectApplication(string receiverEmail, string applicantName,string companyName, string jobPosition);

        void SendEmailOnUpdateApplicantStatus(string receiverEmail, string applicantName, string previousStatus, string newStatus);
        void SendEmailHRApplicationDecision(string receiverEmail, int applicantId,string applicantName, string companyName, string jobPosition);
    }
}
