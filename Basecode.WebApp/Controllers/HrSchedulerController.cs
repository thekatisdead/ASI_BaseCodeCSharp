using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using AutoMapper.Configuration.Conventions;
using Basecode.Data.ViewModels;

namespace Basecode.WebApp.Controllers
{
    public class HrSchedulerController : Controller
    {
        private readonly IEmailSenderService _emailSender;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        IInterviewerServices _interviewerServices;
        IJobOpeningService _jobOpeningService;
        IScheduleService _scheduleService;
        public HrSchedulerController(IInterviewerServices services,IJobOpeningService jobOpeningService,IScheduleService scheduleService,IEmailSenderService emailSender) 
        { 
            _interviewerServices= services;
            _jobOpeningService= jobOpeningService;
            _scheduleService =  scheduleService;
            _emailSender = emailSender;
        }
        public IActionResult AddInterviewer()
        {
            _logger.Trace("HrScheduler Controller Accessed");
            return View();
        }

        public IActionResult Schedule(HrScheduler HrScheduler)
        {
            _emailSender.SendEmailInterviewSchedule(HrScheduler);
            return RedirectToAction("Index");
        }
        public IActionResult home()
        {
            var schedules = _scheduleService.GetDetails();
           
            return View(schedules);
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
            var schedule = new ScheduleViewModel();
            schedule.JobOpenings = _jobOpeningService.RetrieveAll();
            schedule.Interviewers = _interviewerServices.GetAll();
            return View(schedule);
        }
        public IActionResult AddSchedule(Schedule schedule)
        {
            DateTime.TryParseExact(schedule.StartTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startTime);
            DateTime.TryParseExact(schedule.EndTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endTime);
            schedule.StartTime = startTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            schedule.EndTime = endTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            _scheduleService.Add(schedule);
            return RedirectToAction("home", "HrScheduler");
        }
        public IActionResult EditSchedule(int id)
        {  
            var schedule = _scheduleService.GetById(id);
            schedule.Interviewers = _interviewerServices.GetAll();
            schedule.JobOpenings = _jobOpeningService.RetrieveAll();

            DateTime.TryParseExact(schedule.StartTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startTime);
            DateTime.TryParseExact(schedule.EndTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endTime);

            schedule.StartTime = startTime.ToString("HH:mm");
            schedule.EndTime = endTime.ToString("HH:mm");

            return View(schedule);
        }
        public IActionResult UpdateSchedule(Schedule schedule)
        {
         
                
                DateTime.TryParseExact(schedule.StartTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startTime);
                DateTime.TryParseExact(schedule.EndTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endTime);
               
                schedule.StartTime = startTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                schedule.EndTime = endTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                _scheduleService.UpdateSchedule(schedule);  
                return RedirectToAction("home", "HrScheduler");

        }
        public IActionResult DeleteSchedule(int id) 
        { 
            _scheduleService.DeleteSchedule(id);
            return RedirectToAction("home", "HrScheduler");
        }
        [HttpPost]
        public IActionResult ApplicantList()
        {
            return View();
        }
    }
}
