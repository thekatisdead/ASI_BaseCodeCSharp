using Basecode.Services.Interfaces;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Microsoft.AspNetCore.Authorization;
using static Basecode.Data.Constants;

namespace Basecode.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "HR")]
    public class JobOpeningController : Controller
    {
        private readonly IJobOpeningService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public JobOpeningController(IJobOpeningService service)
        {
            _service = service;
        }

        public IActionResult JobList()
        {
            _logger.Trace("JobList action called");
            var data = _service.RetrieveAll();
            return View(data);
        }

        public IActionResult JobPosting()
        {
            _logger.Trace("JobPosting action called");
            return View();
        }

        [HttpPost]
        public IActionResult Add(JobOpening jobOpening)
        {
            _logger.Info("Add action called");
            try
            {
                _service.Add(jobOpening);
                _logger.Info("Job opening added successfully.");
                return RedirectToAction("JobList", "JobOpening");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding job opening.");
                return RedirectToAction("JobPosting");
            }
        }

        public IActionResult UpdateJob(int id)
        {
            _logger.Trace("UpdateJob action called");
            var data = _service.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(JobOpening jobOpening)
        {
            _logger.Info("Update action called");
            try
            {
                _service.Update(jobOpening);
                _logger.Info("Job opening updated successfully.");
                return RedirectToAction("JobList", "JobOpening");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating job opening.");
                return RedirectToAction("UpdateJob", new { id = jobOpening.Id });
            }
        }

        public IActionResult Delete(int id)
        {
            _logger.Info("Delete action called");
            try
            {
                _service.Delete(id);
                _logger.Info("Job opening deleted successfully.");
                return RedirectToAction("JobList", "JobOpening");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting job opening.");
                return RedirectToAction("JobList", "JobOpening");
            }
        }

        public IActionResult DeleteJob(int id)
        {
            _logger.Trace("DeleteJob action called");
            var data = _service.GetById(id);
            return View(data);
        }
    }
}