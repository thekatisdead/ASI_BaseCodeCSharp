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
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            _service.AddForm(viewModel);
            //TempData["SuccessMessage"] = "Successfully created an account! You may proceed to log in to our system.";
            return RedirectToAction("Index");
        }
    }
}
