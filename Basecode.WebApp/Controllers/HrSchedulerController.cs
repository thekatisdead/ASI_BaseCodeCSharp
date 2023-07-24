﻿using Microsoft.AspNetCore.Mvc;
using NLog;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using AutoMapper.Configuration.Conventions;
using Basecode.Data.ViewModels;

namespace Basecode.WebApp.Controllers
{
    public class HrSchedulerController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        IInterviewerServices _interviewerServices;
        IJobOpeningService _jobOpeningService;
        IScheduleService _scheduleService;
        public HrSchedulerController(IInterviewerServices services,IJobOpeningService jobOpeningService,IScheduleService scheduleService) 
        { 
            _interviewerServices= services;
            _jobOpeningService= jobOpeningService;
            _scheduleService =  scheduleService;
        }
        public IActionResult AddInterviewer()
        {
            _logger.Trace("HrScheduler Controller Accessed");
            return View();
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
    }
}
