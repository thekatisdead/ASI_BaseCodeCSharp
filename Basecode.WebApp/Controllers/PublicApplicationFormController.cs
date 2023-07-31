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
            ViewBag.JobId = jobId;
            _logger.Trace("PublicApplicationForm Controller Accessed");
            PublicApplicationFormViewModel form = new PublicApplicationFormViewModel();
            form.Position= jobId;
            return View(form);
        }

        [HttpPost]
        public IActionResult AddForm(PublicApplicationFormViewModel viewModel, int jobId)
        {
            try
            {
                // Call the service method to create the form
                // please help do the loop that makes sure that the id is unique sob
                Random randNum = new Random();
                int value = randNum.Next(10000,99999);
                var newApplicant = new Applicant
                {
                    FormID = value,
                    Firstname = viewModel.FirstName,
                    Lastname = viewModel.LastName,
                    EmailAddress = viewModel.EmailAddress,
                    JobApplied = jobId,
                    Tracker = "Application",
                    Grading = "On Going",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                };
                viewModel.ApplicationID = value;

                var fullName = viewModel.LastName + ", " + viewModel.FirstName;
                _applicant.Add(newApplicant);
                _service.AddForm(viewModel);
                _email.SendEmailApplicantGeneration(viewModel.EmailAddress,fullName,value,viewModel.Position.ToString());

                // add email here

                _logger.Info("Form added successfully.");

                // Redirect or show a success message to the user
                return RedirectToAction("Index", "Home");
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
