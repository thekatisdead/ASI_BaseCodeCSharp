﻿using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Basecode.Data.ViewModels;
using NLog;
using Hangfire;
using NLog.Fluent;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        private readonly IApplicantListService _service;
        private readonly IEmailSenderService _email;
        private readonly ITeamsService _teamsService;
        private readonly CurrentHiresRepository _repository;
        private readonly UserRepository _users;
        private readonly JobOpeningRepository _job;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IPublicApplicationFormService _publicApplicationFormService;

        public ApplicantListController(IApplicantListService service, ITeamsService teamsService, IPublicApplicationFormService publicApplicationFormService,IEmailSenderService email, JobOpeningRepository job, UserRepository users, CurrentHiresRepository repository)
        {
            _service = service;
            _email = email;
            _teamsService = teamsService;
            _job = job;
            _users = users;
            _repository = repository;
            _publicApplicationFormService = publicApplicationFormService;      
        }

        /// <summary>
        /// Displays the index view for the applicant list.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            // test for Hangfire
            //BackgroundJob.Enqueue(()=> _email.SendEmailCharacterReference("kaherbieto@outlook.up.edu.ph","Dat Kat","Kat Dat"));
            var data = _service.RetrieveAll();
            _logger.Trace("ApplicantList Controller Accessed");
            return View(data);
        }

        /// <summary>
        /// Takes in the Applicant ID and then use it to locate the 
        /// row of the Applicant. Updates the Status accordingly
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult UpdateStatus(int applicantID, string status)
        {
            var data = _service.RetrieveAll().FirstOrDefault(a => a.Id == applicantID);
            if (data == null)
            {
                return NotFound(); // or handle the case when applicant is not found
            }

            // get the data from the models
            // getting name from applicant list
            // get the Id so that we can use that to locate the HR
            // use ID to find the HR to send the email
            var _fullName = data.Lastname + " " + data.Firstname;
            var job = _job.GetById(data.JobApplied);
            //var _receiver = _users.FindById((job.HR).ToString());

            _logger.Trace("Updating Status");
            // sends an update whenever the applicant status is changed
            //_email.SendEmailOnUpdateApplicantStatus(_receiver.Address,_fullName,data.Tracker,status);
            _email.SendEmailOnUpdateApplicantStatus("kaherbieto@outlook.up.edu.ph", _fullName, data.Tracker, status);
            _service.ProceedTo(applicantID, status);

            // needs to check if the currentHires exist
            if (status == "Hired")
            {
                _repository.AddHire(applicantID,data.JobApplied);
            }
            else if(status == "shortlisted")
            {

                //_email.SendEmailHRApplicationDecision(_receiver.Address,applicantID,_fullName,job.Position);
                _email.SendEmailHRApplicationDecision("kaherbieto@outlook.up.edu.ph", applicantID, _fullName, job.Position);
            }
            Applicant applicant = new Applicant();
            _service.UpdateStatus(applicantID,status);
            return RedirectToAction("Index", "ApplicantList");
        }

        public IActionResult ViewProfile(int id)
        {
            var applicant = _publicApplicationFormService.GetById(id);
            return View(applicant);
        }
        public IActionResult DownloadCV(byte[] cv)
        {
            byte[] cvBytes = cv;

            // Provide the appropriate content type for the PDF file
            string contentType = "application/pdf";

            // Generate a unique file name for the PDF file
            string fileName = Guid.NewGuid().ToString() + ".pdf";

            // Get the file path for saving the PDF file on the server
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", fileName);

            // Save the byte array as a PDF file on the server
            System.IO.File.WriteAllBytes(filePath, cvBytes);

            // Create a new tab and open the PDF file in the browser
            return Content($"<script>window.open('{Url.Content("~/PDFs/" + fileName)}', '_blank');</script>", "text/html");
        }

        /// Interview functions
        /// These are functions that are used in emails and not in the code
        /// so even though they are unreferenced, they are certainly used
        public IActionResult Reject(int applicantID, string status)
        {
            var data = _service.RetrieveAll().FirstOrDefault(a => a.Id == applicantID);
            var _fullName = data.Lastname + " " + data.Firstname;
            var job = _job.GetById(data.JobApplied);
            if (data == null)
            {
                return NotFound(); // or handle the case when applicant is not found
            }
            _logger.Trace("GRRR");
            var _tempStatus = data.Tracker; // get the job stage
            _logger.Trace($"{data.JobApplied},{data.EmailAddress},{data.Grading},{data.Tracker}");
            if (_tempStatus.ToLower().Contains("interview"))
            {
                status = "Interview";
            }
            else if (_tempStatus.ToLower().Contains("shortlist") || _tempStatus.ToLower().Contains("application"))
            {
                status = "Application";
            }

            _service.UpdateGrade(applicantID, "Rejected");
            if(status == "Application")
            {
                _email.SendEmailRejectApplication(data.EmailAddress,_fullName,job.Position);
            }
            else if(status == "Interview")
            {
                _email.SendEmailRejectInterview(data.EmailAddress,_fullName, job.Position);
            }
            else
            {
                _logger.Trace($"Applicant was Rejected but there was no instance of a [{status}]");
                return NotFound();
            }

            return View();
        }
        public IActionResult Accept(int applicantID)
        {
            return this.UpdateStatus(applicantID, "For HR Interview");
           // _service.ProceedTo(applicantID, "For HR Interview");
        }

        public IActionResult AcceptInterview(int applicantID)
        {
            _service.UpdateGrade(applicantID, "Passed");
            // _service.ProceedTo(applicantID, "For HR Interview");
            return View();
        }

        

    }
}
