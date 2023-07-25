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
        
        public PublicApplicationFormController(IPublicApplicationFormService service, IEmailSenderService email)
        {
            _service = service;
            _email = email;
        }

        public IActionResult Index()
        {
            _logger.Trace("PublicApplicationForm Controller Accessed");
            return View();
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            try
            {
                // time that it takes for the function to execute
                var dueTime = DateTime.UtcNow.AddHours(48);

                // contacts the references for each thting when creating the form
                if (viewModel.ContactInfoOne != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoOne,viewModel.LastName,viewModel.ReferenceOneFullName);
                    
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    BackgroundJob.Schedule(()=>_email.SendEmailCharacterReferenceReminder(viewModel.ContactInfoOne,viewModel.LastName,viewModel.ReferenceOneFullName,viewModel.PositionType),dueTime);
                }
                else if (viewModel.ContactInfoTwo != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoTwo, viewModel.LastName, viewModel.ReferenceTwoFullName);
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    // also change the variable names, handled in a seperate function
                    BackgroundJob.Schedule(() => _email.SendEmailCharacterReferenceReminder(viewModel.ContactInfoOne, viewModel.LastName, viewModel.ReferenceOneFullName, viewModel.PositionType), dueTime);
                }
                else if (viewModel.ContactInfoThree != null)
                {
                    _email.SendEmailCharacterReference(viewModel.ContactInfoThree, viewModel.LastName, viewModel.ReferenceThreeFullName);
                    // replace the _email function with a seperate function that checks if the thing has responded na
                    // also change the variable names, handled in a seperate function
                    BackgroundJob.Schedule(() => _email.SendEmailCharacterReferenceReminder(viewModel.ContactInfoOne, viewModel.LastName, viewModel.ReferenceOneFullName, viewModel.PositionType), dueTime);
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
                return BadRequest("An error occurred while adding the form.");
            }
        }


    }
}
