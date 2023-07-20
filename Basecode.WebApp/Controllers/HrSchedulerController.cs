using Microsoft.AspNetCore.Mvc;
using NLog;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class HrSchedulerController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        IInterviewerServices _interviewerServices;
        public HrSchedulerController(IInterviewerServices services) 
        { 
            _interviewerServices= services;
        }
        public IActionResult AddInterviewer()
        {
            _logger.Trace("HrScheduler Controller Accessed");
            return View();
        }
        public IActionResult home()
        {
            return View();
        }
        public IActionResult Add(Interviewer interviewer)
        {
            _interviewerServices.Add(interviewer);
            return RedirectToAction("InterviewerList", "HrScheduler");
        }
    }
}
