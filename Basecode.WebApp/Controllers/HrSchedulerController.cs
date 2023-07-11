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
            var receiverEmail = "receiver-email@.com";
            var subject = "test";
            var message = "testing testing email email";

            //await _emailSender.SendEmailAsync(receiverEmail, subject, message);
            await _emailSender.SendEmailAsyncWithHtml(receiverEmail, subject);
            return View();
        }
    }
}
