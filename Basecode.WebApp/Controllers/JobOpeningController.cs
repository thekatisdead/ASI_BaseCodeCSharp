using Basecode.Services.Interfaces;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class JobOpeningController : Controller
    {
        private readonly IJobOpeningService _service;

        public JobOpeningController(IJobOpeningService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JobList()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        public IActionResult JobPosting()
        {
            return View();
        }

        public IActionResult UpdateJob(int id)
        {
            var data = _service.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(JobOpening JobOpening)
        {
            _service.Update(JobOpening);
            return RedirectToAction("Index");
        }
    }
}
