using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NuGet.Protocol;
using System;
using Basecode.WebApp.Models;

namespace Basecode.WebApp.Controllers
{
    public class ApplicationTrackingController : Controller
    {
        private readonly ApplicationTrackingRepository _repository;

        public ApplicationTrackingController(ApplicationTrackingRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index(int ApplicantId)
        {

            var applicationTracking = _repository.GetApplicationTracking(ApplicantId);
            if (applicationTracking == null)
            {
                // Set error message in TempData
                TempData["ErrorMessage"] = "Application tracking not found.";

                // Redirect to another action that displays the error popup
                ApplicationTrackingModel model = new ApplicationTrackingModel
                {
                    Id = ApplicantId,
                    FirstName = "Aima",
                    LastName = "Gudgurl",
                    EmailAddress = "",
                    JobApplied = -1,
                    Tracker = "",
                    Grading = "Ongoing",
                };
                return View(model);
            }
            return View(applicationTracking);
            //return View(db);
        }

        public IActionResult ErrorPopup()
        {
            // Retrieve the error message from TempData
            var errorMessage = TempData["ErrorMessage"] as string;

            // Pass the error message to the view
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}
