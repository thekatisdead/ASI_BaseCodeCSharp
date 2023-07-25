﻿using Basecode.Data.Interfaces;
using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Basecode.WebApp.Controllers
{
    public class HrHomepageController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IJobOpeningService _jobOpeningService;
        private readonly IApplicantListService _service;

        public HrHomepageController(IJobOpeningService jobOpeningService, IApplicantListService applicantListService)
        {
            _jobOpeningService = jobOpeningService;
            _service = applicantListService;
        }

        public IActionResult Index(string Username)
        {
            _logger.Trace("HrHomepage Controller Accessed");

            int totalJobOpenings = _jobOpeningService.RetrieveAll()?.Count() ?? 0;
            ViewBag.TotalJobOpenings = totalJobOpenings;

            var data = _service.RetrieveAll();
            int totalApplications = data?.Count() ?? 0; // Fetch the total count of applications
            ViewBag.TotalApplications = totalApplications;

            //// Fetch the recent job opening data from the repository
            //var recentJobOpening = _jobOpeningService.GetMostRecentJobOpening();

            //ViewBag.Name = Username;
            //return View(recentJobOpening); // Pass the recent job opening data to the view

            // Fetch the recent job opening data from the repository
            var recentJobOpening = _jobOpeningService.GetMostRecentJobOpening();

            // Fetch the applicant data from the service
            var applicantsData = _service.GetMostRecentApplicant();

            // Pass the data to the view using a composite view model or ViewBag/ViewData
            // Using composite view model approach
            var compositeViewModel = new CompositeViewModel
            {
                JobOpeningData = recentJobOpening,
                ApplicantsData = applicantsData
            };
            ViewBag.Name = Username;
            return View(compositeViewModel);
        }
    }
}
