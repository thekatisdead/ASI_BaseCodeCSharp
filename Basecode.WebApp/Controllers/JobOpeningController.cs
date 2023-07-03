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
        /// <summary>
        /// View Job List page/screen
        /// </summary>
        /// <returns></returns>
        public IActionResult JobList()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }
        /// <summary>
        /// View Job Posting page/screen
        /// </summary>
        /// <returns></returns>
        public IActionResult JobPosting()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(JobOpening jobOpening)
        {
            _service.Add(jobOpening);
            return RedirectToAction("JobList");
        }

        /// <summary>
        /// Retrieve data from JobOpening table for updating
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateJob(int id)
        {
            var data = _service.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(JobOpening jobOpening)
        {
            _service.Update(jobOpening);
            return RedirectToAction("JobList");
        }
    }
}
