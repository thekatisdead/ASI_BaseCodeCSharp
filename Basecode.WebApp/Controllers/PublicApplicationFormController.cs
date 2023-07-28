using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;
using NLog;
using Hangfire;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;
        private readonly IEmailSenderService _email;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IApplicantListService _applicantListService;
        private readonly IJobOpeningService _jobOpeningService;

        public PublicApplicationFormController(IPublicApplicationFormService service, IEmailSenderService email, IApplicantListService applicantListService, IJobOpeningService jobOpeningService)
        {
            _service = service;
            _email = email;
            _applicantListService = applicantListService;
            _jobOpeningService = jobOpeningService;
        }

        public IActionResult Index(int jobId)
        {
            _logger.Trace("PublicApplicationForm Controller Accessed");
            PublicApplicationFormViewModel form = new PublicApplicationFormViewModel();
            form.Position= jobId;
            return View(form);
        }

        public void EmailCharacterReferenceHandler(int applicantID, int referenceTrigger, string contact, string candidateName, string referenceName, string position)
        {
            var user = _service.GetById(applicantID);

            if (referenceTrigger == 1)
            {
                if(user.AnsweredOne == null)
                {
                    _email.SendEmailCharacterReferenceReminder(contact, candidateName, referenceName, position);
                }
            }
            else if (referenceTrigger == 2)
            {
                if (user.AnsweredTwo == null)
                {
                    _email.SendEmailCharacterReferenceReminder(contact, candidateName, referenceName, position);
                }
            }
            else if (referenceTrigger == 3)
            {
                if (user.AnsweredThree == null)
                {
                    _email.SendEmailCharacterReferenceReminder(contact, candidateName, referenceName, position);
                }
            }
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            try
            {
                // time that it takes for the function to execute
                var dueTime = DateTime.UtcNow.AddHours(48);
                var applicant = _applicantListService.GetApplicantById(viewModel.ApplicantId);
                var job = _jobOpeningService.GetById(viewModel.Position);
                // contacts the references for each thting when creating the form

                if (viewModel.ContactInfoOne != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoOne,applicant.Lastname,viewModel.ApplicantId,viewModel.ReferenceOneFullName);
                    
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    BackgroundJob.Schedule(()=>EmailCharacterReferenceHandler(viewModel.ApplicantId, 1,viewModel.ContactInfoOne, applicant.Lastname, viewModel.ReferenceOneFullName,job.Position),dueTime);
                }
                if (viewModel.ContactInfoTwo != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoTwo, applicant.Lastname, viewModel.ApplicantId, viewModel.ReferenceTwoFullName);
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    // also change the variable names, handled in a seperate function
                    BackgroundJob.Schedule(() => EmailCharacterReferenceHandler(viewModel.ApplicantId, 2, viewModel.ContactInfoTwo, applicant.Lastname, viewModel.ReferenceTwoFullName, job.Position), dueTime);
                }
                if (viewModel.ContactInfoThree != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoThree, applicant.Lastname, viewModel.ApplicantId, viewModel.ReferenceThreeFullName);
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    // also change the variable names, handled in a seperate function
                    BackgroundJob.Schedule(() => EmailCharacterReferenceHandler(viewModel.ApplicantId, 3, viewModel.ContactInfoThree, applicant.Lastname, viewModel.ReferenceThreeFullName, job.Position), dueTime);
                }

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
                return BadRequest("An error occurred while adding the form."+ex.Message);
            }
        }


    }
}
