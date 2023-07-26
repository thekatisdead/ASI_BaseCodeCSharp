
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Basecode.WebApp.Controllers
{
    public class CharacterReferenceController : Controller
    {
        private readonly ICharacterReferenceService _service;
        private readonly IEmailSenderService _email;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CharacterReferenceController(ICharacterReferenceService service, IEmailSenderService email)
        {
            _service = service;
            _email = email;
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
                // needs communication with Public form DB
                _email.SendEmailOnCharacterReferenceResponse("kaherbieto@outlook.up.edu.ph",viewModel.CandidateLastName,viewModel.LastName,1,3);
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

        [Route("CharacterReference/ManageRespondents")]
        public IActionResult ManageRespondents()
        {
            try
            {
                var data = _service.RetrieveAll();
                _logger.Info("Successfully retrieved data. Character reference respondents list generated.");
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while generating the character reference respondents list: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while generating the character reference respondents list.");
            }
        }

        [Route("CharacterReference/Report")]
        public IActionResult GenerateCharacterReferenceReport(int respondentId)
        {
            try
            {
                var respondent = _service.RetrieveResponses().FirstOrDefault(r => r.Id == respondentId);
                if (respondent == null)
                {
                    return NotFound();
                }

                // Retrieve other respondents with the same candidate name
                var relatedRespondents = _service.RetrieveResponses()
                    .Where(r => r.CandidateFirstName == respondent.CandidateFirstName && r.CandidateLastName == respondent.CandidateLastName)
                    .ToList();

                ViewBag.CurrentRespondentId = respondentId;
                return View(relatedRespondents);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while generating the character reference report: {errorMessage}", ex.Message);
                return BadRequest("An error occurred while generating the character reference report.");
            }
        }
    }
}
