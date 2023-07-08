using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using Basecode.Data.Models;
namespace Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IJobOpeningService _jobOpeningService;

        public AdminController(IJobOpeningService jobOpeningService)
        {
            _jobOpeningService= jobOpeningService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminJobListing()
        {
            var job = _jobOpeningService.RetrieveAll();
            return View(job);
        }
    }
}
