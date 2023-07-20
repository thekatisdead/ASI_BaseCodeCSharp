using Microsoft.AspNetCore.Mvc;
using NLog;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using AutoMapper.Configuration.Conventions;

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
        public IActionResult InterviewerList()
        { 
            var interviewers = _interviewerServices.GetAll();
            return View(interviewers); 
        }
        public IActionResult UpdateInterviewer(int id) 
        {
            var interviewer = _interviewerServices.GetById(id);
            return View(interviewer);
        }
        public IActionResult Update(Interviewer interviewer)
        {
            _interviewerServices.Update(interviewer);
            return RedirectToAction("InterviewerList","HrScheduler");
        }
        public IActionResult Delete(int id)
        {
            _interviewerServices.Delete(id);
            return RedirectToAction("InterviewerList","HrScheduler");
        }
        public IActionResult ViewAddSchedule()
        {
            return View();
        }
    }
}
