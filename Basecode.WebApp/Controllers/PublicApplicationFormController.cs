﻿using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;
using NLog;
using Hangfire;
using Basecode.Data.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;
        private readonly IApplicantListRepository _applicant;
        private readonly IEmailSenderService _email;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IApplicantListService _applicantListService;
        private readonly IJobOpeningService _jobOpeningService;

        public PublicApplicationFormController(IPublicApplicationFormService service, IEmailSenderService email, IApplicantListService applicantListService, IJobOpeningService jobOpeningService, IApplicantListRepository applicant)
        {
            _service = service;
            _email = email;
            _applicant = applicant;
            _applicantListService = applicantListService;
            _jobOpeningService = jobOpeningService;
        }

        public IActionResult Index(int jobId)
        {
            _logger.Trace("PublicApplicationForm Controller Accessed");
            PublicApplicationFormViewModel form = new PublicApplicationFormViewModel();
            ViewBag.JobId = jobId;
            return View(form);
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel, int jobId)
        {
            try
            {
                int value = 0;
                // Call the service method to create the form
                
                Random randNum = new Random();
                value = randNum.Next(10000, 99999);
                
                
                var newApplicant = new Applicant
                {
                    FormId = value,
                    Firstname = viewModel.FirstName,
                    Lastname = viewModel.LastName,
                    EmailAddress = viewModel.EmailAddress,
                    JobApplied = jobId,
                    Tracker = "Application",
                    Grading = "On Going",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                };
                viewModel.ApplicationID = value;

                var fullName = viewModel.LastName + ", " + viewModel.FirstName;
                var job = _jobOpeningService.GetById(jobId);
                viewModel.Position = job.Position;
                _applicant.Add(newApplicant);
                _service.AddForm(viewModel);
                // for applicant
                _email.SendEmailApplicantGeneration(viewModel.EmailAddress,fullName,value,job.Position);
                
                // for HR
                _email.SendEmailApplicantGenerationHR(job.HREmail, fullName, value, job.Position);

                _logger.Info("Form added successfully.");

                // Redirect or show a success message to the user
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the error using a logger
                _logger.Error(ex, "Error occurred while adding character reference: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while adding the form."+ex.Message);
            }
        }


    }
}
