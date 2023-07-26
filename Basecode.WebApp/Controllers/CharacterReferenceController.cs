
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class CharacterReferenceController : Controller
    {
        private readonly ICharacterReferenceService _service;
        private readonly IPublicApplicationFormService _applicationForm;
        private readonly IEmailSenderService _email;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CharacterReferenceController(ICharacterReferenceService service, IEmailSenderService email, IPublicApplicationFormService applicationForm)
        {
            _service = service;
            _email = email;
            _applicationForm = applicationForm;
        }

        public IActionResult Index(int applicantID)
        {
            _logger.Trace("CharacterReference Controller Accessed");
            ViewData["ApplicantID"] = applicantID;
            return View();
        }

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="viewModel">The character reference view model.</param>
        /// <returns>A redirect to the index action.</returns>
        public IActionResult Add(CharacterReferenceViewModel viewModel, int applicantID)
        {
            try
            {
                var _numberAnswered = _applicationForm.CountResponded(viewModel.Id);
                if( _numberAnswered < 3 ) 
                {
                    _email.SendEmailOnCharacterReferenceResponse("kaherbieto@outlook.up.edu.ph", viewModel.CandidateLastName, viewModel.LastName, _numberAnswered, 3);
                }
                else
                {
                    // needs communication on when to send the decision, after all are completed or after a certain period of time
                    _email.SendEmailCharacterReferenceDecision("kaherbieto@outlook.up.edu.ph", viewModel.LastName, applicantID, viewModel.Position);
                }
                

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
        public IActionResult DecisionLanding()
        {
            return View();
        }

        public IActionResult Decision(int applicantID, int decision)
        {
            try
            {
                // thing change in the database
                return RedirectToAction("DecisionLanding");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while in the rejection phase of the Character Reference From: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while generating the report.");
            }
        }
    }
}
