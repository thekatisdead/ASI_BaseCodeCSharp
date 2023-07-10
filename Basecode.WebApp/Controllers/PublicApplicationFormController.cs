using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;
        
        public PublicApplicationFormController(IPublicApplicationFormService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            // Call the service method to create the form
            _service.AddForm(viewModel);

            // Redirect or show a success message to the user
            return RedirectToAction("Index", "ApplicantHomepage");
        }

    }
}
