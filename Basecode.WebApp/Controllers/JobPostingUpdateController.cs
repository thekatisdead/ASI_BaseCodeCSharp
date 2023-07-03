using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class JobPostingUpdateController : Controller
    {
        private readonly IJobOpeningService _service;

        public JobPostingUpdateController(IJobOpeningService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var JobOpening = _service.GetById(id);
            return View(JobOpening);
        }

        [HttpPost]
        public IActionResult Update(JobOpening JobOpening)
        {
            _service.Update(JobOpening);
            return RedirectToAction("Index", "JobListing");
        }
    }
}
