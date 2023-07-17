using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantHomepageController : Controller
    {
        private readonly IJobOpeningService _jobOpeningService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicantHomepageController(IJobOpeningService jobOpeningService)
        {
            _jobOpeningService = jobOpeningService;
        }
        public IActionResult Index()
        {
            _logger.Trace(" ApplicantHomepage Controller Accessed");
            return View();
        }
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