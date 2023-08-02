using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using NLog;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantHomepageController : Controller
    {
        private readonly IJobOpeningService _jobOpeningService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IApplicantListService _applicantListService;

        public ApplicantHomepageController(IJobOpeningService jobOpeningService,IApplicantListService applicantListService)
        {
            _jobOpeningService = jobOpeningService;
            _applicantListService = applicantListService;
        }

        /// <summary>
        /// Displays the applicant's homepage.
        /// </summary>
        /// <returns>The Index view with the applicant's details.</returns>
        public IActionResult Index()
        {
            //the id variable is used to get an applicant from the Applicant table
            //you may delete this variable once routing is complete.
            int id = 2;
            var applicant = _applicantListService.GetApplicantById(id);

            //This line saves the applicant ID althroughout the pages.
            //If the applicant logs out, make sure to clear the session.
            HttpContext.Session.SetInt32("Key",applicant.Id);
            _logger.Trace(" ApplicantHomepage Controller Accessed");
            return View(applicant);
        }

        /// <summary>
        /// Displays the list of job openings available.
        /// </summary>
        /// <returns>The JobListing view with the list of job openings.</returns>
        public IActionResult JobListing()
        {
            try
            {
                var jobs = _jobOpeningService.RetrieveAll();

                // Log the number of retrieved job openings
                _logger.Info("Retrieved {jobCount} job openings", jobs.Count);

                return View(jobs);
            }
            catch (Exception ex)
            {
                // Log the error using a logger
                _logger.Error(ex, "Error occurred while retrieving job listings: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while retrieving the job openings.");
            }
        }

    }
}