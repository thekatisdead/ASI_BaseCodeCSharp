using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class JobListingController : Controller
    {
        private readonly IJobOpeningService _service;
        public JobListingController(IJobOpeningService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult UpdateView(int id)
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
