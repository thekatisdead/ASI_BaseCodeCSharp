using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.ViewModels;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        private readonly IApplicantListService _service;
        private readonly CurrentHiresRepository _repository;
        private readonly IPublicApplicationFormService _publicApplicationFormService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicantListController(IApplicantListService service,IPublicApplicationFormService publicApplicationFormService)
        {
            _service = service;
            _publicApplicationFormService = publicApplicationFormService;      
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
        public IActionResult ViewProfile(int id)
        {
            var applicant = _publicApplicationFormService.GetById(id);
            return View(applicant);
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
            Applicant applicant = new Applicant();
            _service.UpdateStatus(applicantID,status);
            return RedirectToAction("Index", "ApplicantList");
        }
        public IActionResult DownloadCV(byte[] cv)
        {
            byte[] cvBytes = cv;

            // Provide the appropriate content type for the PDF file
            string contentType = "application/pdf";

            // Generate a unique file name for the PDF file
            string fileName = Guid.NewGuid().ToString() + ".pdf";

            // Get the file path for saving the PDF file on the server
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", fileName);

            // Save the byte array as a PDF file on the server
            System.IO.File.WriteAllBytes(filePath, cvBytes);

            // Create a new tab and open the PDF file in the browser
            return Content($"<script>window.open('{Url.Content("~/PDFs/" + fileName)}', '_blank');</script>", "text/html");
        }
    }
}
