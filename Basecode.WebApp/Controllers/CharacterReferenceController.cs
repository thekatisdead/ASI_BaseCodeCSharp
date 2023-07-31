
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
        private IPublicApplicationFormService _applicationForm;
        private readonly IApplicantListService _applicantRepository;
        private readonly IEmailSenderService _email;
        private IJobOpeningService _jobOpeningService;
        private IUserService _userService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CharacterReferenceController(ICharacterReferenceService service, IEmailSenderService email, IPublicApplicationFormService applicationForm, IJobOpeningService jobOpeningService, IUserService userService)
        {
            _service = service;
            _email = email;
            _applicationForm = applicationForm;
            _jobOpeningService = jobOpeningService;
            _userService = userService;
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
        public IActionResult Add(CharacterReferenceViewModel viewModel, int applicantID, int trigger)
        {
            _applicationForm.Responded(applicantID);
            // problem is that registration no user and thing
            var _applicant = _applicantRepository.GetApplicantById(applicantID);
            var _hrEmail = _userService.FindById(_applicant.JobApplied.ToString()).Email;
            try
            {
                _logger.Trace("flag 0 passed");
                //var _numberAnswered = _applicationForm.CountResponded(viewModel.Id);
                var _numberAnswered = _applicationForm.CountResponded(applicantID); // temp variable because CountResponded is wrong apparently
                _logger.Trace("flag 1 passed");
                // sends an email to the HR
                if ( _numberAnswered < 3 ) 
                {
                    _email.SendEmailOnCharacterReferenceResponse(_hrEmail, viewModel.CandidateLastName, viewModel.LastName, _numberAnswered, 3);
                }
                else
                {
                    // needs communication on when to send the decision, after all are completed or after a certain period of time
                    _email.SendEmailCharacterReferenceDecision(_hrEmail, viewModel.LastName, applicantID, viewModel.Position);
                }
                // sends an email to the form
                _logger.Trace("flag 2 passed");
                var user = _applicationForm.GetByApplicationId(_applicantRepository.GetApplicantById(applicantID).FormID);
                // email changes here but it needs to be connected :sob:
                var _emailCharacter = "scape";

                if(trigger == 1)
                {
                    _emailCharacter = user.ContactInfoOne;
                }
                else if (trigger == 2)
                {
                    _emailCharacter = user.ContactInfoTwo;
                }
                else if (trigger == 1)
                {
                    _emailCharacter = user.ContactInfoThree;
                }
                _email.SendEmailCharacterReferenceGratitude(_emailCharacter, viewModel.LastName, viewModel.Position, viewModel.CandidateLastName);
                _logger.Trace("flag 3 passed");


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
