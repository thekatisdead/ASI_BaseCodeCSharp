using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantHomepageController : Controller
    {
        private readonly IJobOpeningService _jobOpeningService;
        public ApplicantHomepageController(IJobOpeningService jobOpeningService)
        {
            _jobOpeningService = jobOpeningService;
        }
        public IActionResult Index()
        {
            return View();
        }
       public IActionResult JobListing() 
        {
            var jobs = _jobOpeningService.RetrieveAll();
            return View(jobs);  
        }
    }
}
