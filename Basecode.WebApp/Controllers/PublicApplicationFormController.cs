using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;
        private PublicApplicationFormViewModel? _viewModel; // Member variable to store the view model

        public PublicApplicationFormController(IPublicApplicationFormService service)
        {
            _service = service;
            _viewModel = new PublicApplicationFormViewModel();
        }

        [Route("public-application-form")]
        public IActionResult Index()
        {
            return View(_viewModel);
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            // Update the view model with the submitted data
            _viewModel.Achievements = viewModel.Achievements;
            _viewModel.ReferenceOneFullName = viewModel.ReferenceOneFullName;
            _viewModel.RelationshipOne = viewModel.RelationshipOne;
            _viewModel.ContactInfoOne = viewModel.ContactInfoOne;
            _viewModel.ReferenceTwoFullName = viewModel.ReferenceTwoFullName;
            _viewModel.RelationshipTwo = viewModel.RelationshipTwo;
            _viewModel.ContactInfoTwo = viewModel.ContactInfoTwo;
            _viewModel.ReferenceThreeFullName = viewModel.ReferenceThreeFullName;
            _viewModel.RelationshipThree = viewModel.RelationshipThree;
            _viewModel.ContactInfoThree = viewModel.ContactInfoThree;
            _viewModel.CurriculumVitae = viewModel.CurriculumVitae;

            // Call the service method to create the form
            _service.AddForm(_viewModel);

            // Redirect or show a success message to the user
            return RedirectToAction("Index");
        }
    }
}
