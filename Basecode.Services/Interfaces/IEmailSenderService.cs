using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string receiverEmail, string subject, string message);
        Task SendEmailAsyncWithHtml(string receiverEmail, string subject);

        Task SendEmailRejectApplication(string receiverEmail, string companyName, string jobPosition);

        Task SendEmailOnUpdateApplicantStatus(string receiverEmail, string applicantName, string previousStatus, string newStatus);
    }
}
