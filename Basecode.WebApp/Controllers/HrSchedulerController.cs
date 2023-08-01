using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Hangfire;
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
        IUserService _userService;
        
        public HrSchedulerController(IInterviewerServices services,IJobOpeningService jobOpeningService,IScheduleService scheduleService,IEmailSenderService emailSender, IUserService userService) 
        { 
            _interviewerServices= services;
            _jobOpeningService= jobOpeningService;
            _scheduleService =  scheduleService;
            _emailSender = emailSender;
            _userService = userService;
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
            if (interviewer == null)
            {
                return NotFound(); // Return a 404 Not Found result when the interviewer is not found
            }
            return View(interviewer);
        }
        public IActionResult Update(Interviewer interviewer)
        {
            _interviewerServices.Update(interviewer);
            return RedirectToAction("InterviewerList","HrScheduler");
        }
        public IActionResult Delete(int id)
        {
            var interviewer = _interviewerServices.GetById(id);
            if (interviewer == null)
            {
                return NotFound(); // Return NotFound result if the interviewer is not found
            }

            _interviewerServices.Delete(id);
            return RedirectToAction("InterviewerList", "HrScheduler");
        }
        public IActionResult ViewAddSchedule()
        {
            var schedule = new ScheduleViewModel();

            // Get job openings and interviewers
            var jobOpenings = _jobOpeningService.RetrieveAll();
            var interviewers = _interviewerServices.GetAll();
            if (jobOpenings == null && interviewers == null)
            {
                return BadRequest("No job openings and interviewers available.");
            }
            schedule.JobOpenings = jobOpenings;
            schedule.Interviewers = interviewers;

            return View(schedule);
        }
        public IActionResult AddSchedule(Schedule schedule)
        {
            _logger.Trace(schedule.InterviewerId);
            var interviewer = _interviewerServices.GetById(schedule.InterviewerId);
            var date = DateTime.Parse(schedule.Date);
            var time = TimeSpan.Parse(schedule.EndTime);
            var combinedDateTime = date.Add(time);
            var delay = combinedDateTime - DateTime.Now;
            var interviewerName = interviewer.LastName + " " + interviewer.FirstName;

            // sends an email for the interviewer after the discussion
            // if you want to cancel an email, we have to like create another table
            // this will be in the suggestions

            BackgroundJob.Schedule(() => _emailSender.SendEmailInterviewDecision(interviewer.Email, interviewerName, "Alliance Software Inc.", schedule.JobId.ToString()),delay);
            //_emailSender.SendEmailInterviewDecision(interviewer.Email,interviewerName,"Alliance Software Inc.",schedule.JobId.ToString());
            
            // requires the name of the applicant too
            //_emailSender.SendEmailInterviewGeneration(interviewer.Email,interviewer.LastName,"Molly",1,"Bottom",schedule.Date,schedule.);
            
            
            DateTime.TryParseExact(schedule.StartTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startTime);
            DateTime.TryParseExact(schedule.EndTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endTime);
            schedule.StartTime = startTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            schedule.EndTime = endTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            _scheduleService.Add(schedule);
            return Json(new { success = true, message = "Form data received successfully." });
        }

        //Adding schedule to the Scheduletable for the Modal.
        //pasensya sa taas kaayu nga parameters, kay gi manual man nakoug bind hehehe
        public IActionResult AddScheduleModal(List<int> applicants,string InterviewerId,string JobId, string StartTime, string EndTime, string Date, string ExamType, string Instruction, string TeamsLink)
        {
            Schedule schedule = new Schedule();
           
            schedule.InterviewerId = int.Parse(InterviewerId);
            schedule.JobId = int.Parse(JobId);

            DateTime.TryParseExact(schedule.StartTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startTime);
            DateTime.TryParseExact(schedule.EndTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endTime);
            schedule.StartTime = startTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            schedule.EndTime = endTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

            schedule.Date = Date;
            schedule.ExamType = ExamType;
            schedule.Instruction = Instruction;
            schedule.TeamsLink = TeamsLink;
            _scheduleService.Add(schedule);

            int id = _scheduleService.GetMostRecentSchedId();

            foreach(var app in applicants)
            {
                var appsched = new ApplicantsSchedule();
                appsched.ApplicantId = app;
                appsched.ScheduleId = id;
                _scheduleService.AddApplicantSchedule(appsched);
            }
            return Json(new { success = true, message = "Form data received successfully." });
        }
        public IActionResult EditSchedule(int id)
        {
            var schedule = _scheduleService.GetById(id);

            if (schedule == null)
            {
                return NotFound();
            }

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
        [HttpPost]
        public IActionResult ViewApplicantsAccordingToJobApplied(int job)
        {
            var applicants =_scheduleService.GetApplicantListAccordingToJobApplied(job);
            return Json(applicants);
        }
        [HttpPost]
        public IActionResult GetApplicantListAccordingToSchedule(int schedule)
        {
            var applicants = _scheduleService.GetApplicantListAccordingToSchedule(schedule);
            return Json(applicants);
        }
        [HttpPost]
        public IActionResult GetInterviewers()
        {
            var interviewers = _scheduleService.GetInterviewersServ();
            return Json(interviewers);
        }
        [HttpPost]
        public IActionResult GetJobs()
        {
            var jobs = _scheduleService.GetJobs();
            return Json(jobs);
        }
    }
}
