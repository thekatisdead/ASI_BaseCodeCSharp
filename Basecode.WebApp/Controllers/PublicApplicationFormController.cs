using Basecode.Services.Interfaces;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;
using NLog;

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

        public IActionResult Index(int jobId)
        {
            _logger.Trace("PublicApplicationForm Controller Accessed");
            PublicApplicationFormViewModel form = new PublicApplicationFormViewModel();
            form.Position= jobId;
            return View(form);
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel)
        {
            try
            {
                // contacts the references for each thting when creating the form
                //if(viewModel.ContactInfoOne != null)
                //{
                //    _email.SendEmailCharacterReference(viewModel.ContactInfoOne,viewModel.LastName,viewModel.ReferenceOneFullName);
                //}
                //else if (viewModel.ContactInfoTwo != null)
                //{
                //    _email.SendEmailCharacterReference(viewModel.ContactInfoTwo, viewModel.LastName, viewModel.ReferenceTwoFullName);
                //}
                //else if (viewModel.ContactInfoThree != null)
                //{
                //    _email.SendEmailCharacterReference(viewModel.ContactInfoThree, viewModel.LastName, viewModel.ReferenceThreeFullName);
                //}
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
