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
    }
}
