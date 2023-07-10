using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;

        public PublicApplicationFormController(IPublicApplicationFormService service)
        {
            _service = service;
        }

        [Route("public-application-form")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("page2-public-application-form")]
        public IActionResult IndexTwo(PublicApplicationFormViewModel viewModel)
        {
            // Validate the submitted data if necessary

            // Pass the data to the next view
            return View("IndexTwo", viewModel);
        }

        [HttpPost]
        [Route("page3-public-application-form")]
        public IActionResult IndexThree(PublicApplicationFormViewModel viewModel)
        {
            // Validate the submitted data if necessary

            // Call the service method to create the form
            //_service.AddForm(viewModel);

            // Redirect or show a success message to the user
            return View("IndexThree", viewModel);
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            _service.AddForm(viewModel);
            //TempData["SuccessMessage"] = "Successfully created an account! You may proceed to log in to our system.";
            return RedirectToAction("Index");
        }
    }
}
