using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        private readonly IApplicantListService _service;
        private readonly CurrentHiresRepository _repository;

        public ApplicantListController(IApplicantListService service)
        {
            _service = service;
        }

        /// <summary>
        /// Displays the index view for the applicant list.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        /// <summary>
        /// Takes in the Applicant ID and then use it to locate the 
        /// row of the Applicant. Updates the Status accordingly
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult UpdateStatus(int applicantID, string status)
        {
            var data = _service.RetrieveAll().FirstOrDefault(a => a.Id == applicantID);
            if (data == null)
            {
                return NotFound(); // or handle the case when applicant is not found
            }
            data.Tracker = status;

            // needs to check if the currentHires exist
            if (status == "Hired")
            {
                _repository.AddHire(applicantID,data.JobApplied);
            }

            var _allData = _service.RetrieveAll();
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int applicantID)
        {
            var data = _service.RetrieveAll().FirstOrDefault(a => a.Id == applicantID);
            if (data == null)
            {
                return NotFound(); // or handle the case when applicant is not found
            }
            data.Grading = "Rejected";

            return View();
        }
    }
}
