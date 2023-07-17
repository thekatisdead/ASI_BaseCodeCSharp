using Basecode.Data.Models;
using Basecode.WebApp.Models;
using Basecode.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NuGet.Protocol;
using System;
using Basecode.Data.Interfaces;
using Basecode.Main.Models;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ApplicationTrackingController : Controller
    {
        private readonly ApplicationTrackingRepository _repository;
        private readonly JobOpeningRepository _jobRepository;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicationTrackingController(ApplicationTrackingRepository applicationRepository, JobOpeningRepository jobOpeningRepository)
        {
            _repository = applicationRepository;
            _jobRepository = jobOpeningRepository;
        }
        public IActionResult Index(int ApplicantId)
        {

            var applicationTracking = _repository.GetApplicationTracking(ApplicantId);
            if (applicationTracking == null)
            {
                // Set error message in TempData
                TempData["ErrorMessage"] = "Application tracking not found.";

                // Redirect to another action that displays the error popup
                ApplicationTrackingModel model = new ApplicationTrackingModel
                {
                    Id = ApplicantId,
                    FirstName = "Aima",
                    LastName = "Gudgurl",
                    EmailAddress = "",
                    JobApplied = -1,
                    Tracker = "",
                    Grading = "Ongoing",
                };
                return View(model);
            }

            var jobOpening = _jobRepository.GetById(applicationTracking.JobApplied);

            if (jobOpening == null)
            {
                // Set error message in TempData
                TempData["ErrorMessage"] = "Job Opening not found.";
                ApplicationTrackingModel model = new ApplicationTrackingModel
                {
                    Id = ApplicantId,
                    FirstName = "Aima",
                    LastName = "Gudgurl",
                    EmailAddress = "",
                    JobApplied = -1,
                    Tracker = "",
                    Grading = "Ongoing",
                };
                return View(model);
            }


            ViewData["jobOpening"] = jobOpening;
            _logger.Trace("ApplicationTracking Controller Accessed");
            return View(applicationTracking);
        }

        public IActionResult ErrorPopup()
        {
            // Retrieve the error message from TempData
            var errorMessage = TempData["ErrorMessage"] as string;

            // Pass the error message to the view
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}
