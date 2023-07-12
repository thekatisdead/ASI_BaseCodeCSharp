using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HrSchedulerController : Controller
    {
        private readonly IEmailSenderService _emailSender;

        public HrSchedulerController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            // change the receiver email to test in your device
            var receiverEmail = "receiver-email@gmail.com";
            var companyName = "Alliance";
            var jobPosition = "Bottom";

            //await _emailSender.SendEmailAsync(receiverEmail, subject, message);
            //await _emailSender.SendEmailAsyncWithHtml(receiverEmail, subject);
            //_emailSender.SendEmailRejectApplication(receiverEmail, "Mr Kat" ,companyName, jobPosition);
            _emailSender.SendEmailHRApplicationDecision(receiverEmail, 1,"Mr Kat" ,companyName, jobPosition);
            //_emailSender.SendEmailOnUpdateApplicantStatus(receiverEmail, "Mr Kat", "Bottom", "Top");
            return View();
        }
    }
}
