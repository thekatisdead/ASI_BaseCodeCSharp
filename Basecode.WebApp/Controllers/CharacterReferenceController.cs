
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class CharacterReferenceController : Controller
    {
        private readonly ICharacterReferenceService _service;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CharacterReferenceController(ICharacterReferenceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            _logger.Trace("CharacterReference Controller Accessed");
            return View();
        }

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="viewModel">The character reference view model.</param>
        /// <returns>A redirect to the index action.</returns>
        public IActionResult Add(CharacterReferenceViewModel viewModel)
        {
            try
            {
                // Call the service method to create the form
                _service.AddCharacterReference(viewModel);

                _logger.Info("Character reference added successfully.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding character reference: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while generating the report.");
            }
        }

        /// <summary>
        /// Generates a character reference report.
        /// </summary>
        /// <returns>The character reference report view.</returns>
        [Route("CharacterReferenceReport")]
        public IActionResult GenerateCharacterReferenceReport()
        {
            try
            {
                var data = _service.RetrieveAll();
                _logger.Info("Successfully retrieve data. Report Generated.");
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while generating character reference report: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while generating the report.");
            }
        }
    }
}
