using Basecode.Data.Models;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Schedule(HrScheduler HrScheduler)
        {
            _emailSender.SendEmailInterviewSchedule(HrScheduler);
            return RedirectToAction("Index");
        }
    }
}
