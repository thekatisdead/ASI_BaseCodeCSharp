using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public PublicApplicationFormController(IPublicApplicationFormService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            _logger.Trace("PublicApplicationForm Controller Accessed");
            return View();
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            try
            {
                // Call the service method to create the form
                _service.AddForm(viewModel);
                _logger.Info("Form added successfully.");

                // Redirect or show a success message to the user
                return RedirectToAction("Index", "ApplicantHomepage");
            }
            catch (Exception ex)
            {
                // Log the error using a logger
                _logger.Error(ex, "Error occurred while adding character reference: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while adding the form.");
            }
        }


    }
}
