using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class CharacterReferenceController : Controller
    {
        private readonly ICharacterReferenceService _service;

        public CharacterReferenceController(ICharacterReferenceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="viewModel">The character reference view model.</param>
        /// <returns>A redirect to the index action.</returns>
        public IActionResult Add(CharacterReferenceViewModel viewModel)
        {
            // Call the service method to create the form
            _service.AddCharacterReference(viewModel);

            // Redirect or show a success message to the user
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Generates a character reference report.
        /// </summary>
        /// <returns>The character reference report view.</returns>
        [Route("CharacterReferenceReport")]
        public IActionResult GenerateCharacterReferenceReport()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }
    }
}
