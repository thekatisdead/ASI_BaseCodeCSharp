using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;
using NLog;
using Hangfire;
using Basecode.Data.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        private readonly IPublicApplicationFormService _service;
        private readonly IApplicantListRepository _applicant;
        private readonly IEmailSenderService _email;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public PublicApplicationFormController(IPublicApplicationFormService service, IEmailSenderService email, IApplicantListRepository applicant)
        {
            _service = service;
            _email = email;
            _applicant = applicant;
        }

        public IActionResult Index()
        {
            _logger.Trace("PublicApplicationForm Controller Accessed");
            return View();
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
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel, int applicantID)
        {
            try
            {
                /*
                // time that it takes for the function to execute
                var dueTime = DateTime.UtcNow.AddHours(48);

                // contacts the references for each thting when creating the form
                if (viewModel.ContactInfoOne != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoOne,viewModel.LastName,applicantID,viewModel.ReferenceOneFullName);
                    
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    BackgroundJob.Schedule(()=>EmailCharacterReferenceHandler(applicantID,1,viewModel.ContactInfoOne,viewModel.LastName,viewModel.ReferenceOneFullName,viewModel.PositionType),dueTime);
                }
                if (viewModel.ContactInfoTwo != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoTwo, viewModel.LastName, applicantID,viewModel.ReferenceTwoFullName);
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    // also change the variable names, handled in a seperate function
                    BackgroundJob.Schedule(() => EmailCharacterReferenceHandler(applicantID, 2, viewModel.ContactInfoTwo, viewModel.LastName, viewModel.ReferenceTwoFullName, viewModel.PositionType), dueTime);
                }
                if (viewModel.ContactInfoThree != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoThree, viewModel.LastName, applicantID,viewModel.ReferenceThreeFullName);
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    // also change the variable names, handled in a seperate function
                    BackgroundJob.Schedule(() => EmailCharacterReferenceHandler(applicantID, 3, viewModel.ContactInfoThree, viewModel.LastName, viewModel.ReferenceThreeFullName, viewModel.PositionType), dueTime);
                }*/
                // Call the service method to create the form
                Random randNum = new Random();
                int value = randNum.Next(10000,99999);
                var newApplicant = new Applicant
                {
                    FormID = value,
                    Firstname = viewModel.FirstName,
                    Lastname = viewModel.LastName,
                    EmailAddress = viewModel.EmailAddress,
                    JobApplied = 0,
                    Tracker = "Application",
                    Grading = "On Going",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                };
                viewModel.ApplicationID = value;
                
                _logger.Trace("Flag 3");
                _applicant.Add(newApplicant);

                _logger.Trace("Flag 2");
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
                return BadRequest("An error occurred while adding the form.");
            }
        }


    }
}
