using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Basecode.Data.ViewModels;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        private readonly IApplicantListService _service;
        private readonly IEmailSenderService _email;
        private readonly ITeamsService _teamsService;
        private readonly CurrentHiresRepository _repository;
        private readonly UserRepository _users;
        private readonly JobOpeningRepository _job;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IPublicApplicationFormService _publicApplicationFormService;

        public ApplicantListController(IApplicantListService service, ITeamsService teamsService, IPublicApplicationFormService publicApplicationFormService,IEmailSenderService email, JobOpeningRepository job, UserRepository users, CurrentHiresRepository repository)
        {
            _service = service;
            _email = email;
            _teamsService = teamsService;
            _job = job;
            _users = users;
            _repository = repository;
            _publicApplicationFormService = publicApplicationFormService;      
        }

        /// <summary>
        /// Displays the index view for the applicant list.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            // temporary thing to test out the thing
            //var scape = _teamsService.GenerateTeamsMeetingLink("Interview HR", 1, 24, "kermherbieto52@gmail.com");

            // test that email service works sob
            //_email.SendEmailInterviewReminder("kaherbieto@outlook.up.edu.ph", "John Cena", "Your Mom", scape, DateTime.Now);

            // reject a person
            //_email.SendEmailRejectApplication("kaherbieto@outlook.up.edu.ph","Kerm Herbieto","Alliance","Bottom");
            //this.Reject(2);

            // update a person's status
            //_email.SendEmailOnUpdateApplicantStatus("kaherbieto@outlook.up.edu.ph","Kerm Herbieto","Bottom","Top");

            // give the HR a decision
            //_email.SendEmailHRApplicationDecision("kaherbieto@outlook.up.edu.ph", 1, "Kerm Herbieto", "Bottom");
            var data = _service.RetrieveAll();
            _logger.Trace("ApplicantList Controller Accessed");
            return View(data);
        }

        /// <summary>
        /// Takes in the Applicant ID and then use it to locate the 
        /// row of the Applicant. Updates the Status accordingly
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult UpdateStatus(int applicantID, string status)
        {
            var data = _service.RetrieveAll().FirstOrDefault(a => a.Id == applicantID);
            if (data == null)
            {
                return NotFound(); // or handle the case when applicant is not found
            }

            // get the data from the models            
            var _fullName = data.Lastname + " " + data.Firstname;
            //var job = _job.GetById(data.JobApplied);
            //var _receiver = _users.FindById((job.HR).ToString());

            //// sends an update whenever the applicant status is changed
            //_email.SendEmailOnUpdateApplicantStatus(_receiver.EmailAddress,_fullName,data.Tracker,status);
            _service.ProceedTo(applicantID, status);

            // needs to check if the currentHires exist
            if (status == "Hired")
            {
                _repository.AddHire(applicantID,data.JobApplied);
            }
            else if(status == "Shortlisted")
            { 
                // WARNING! The problem with this code is I think that the repositories are empty
                // This can be triggered every time the status is different but it does not get the
                // data in the models
                
                //_email.SendEmailHRApplicationDecision(_receiver.EmailAddress,applicantID,_fullName,job.Position);

            }
            Applicant applicant = new Applicant();
            _service.UpdateStatus(applicantID,status);
            return RedirectToAction("Index", "ApplicantList");
        }

        public IActionResult ViewProfile(int id)
        {
            var applicant = _publicApplicationFormService.GetById(id);
            return View(applicant);
        }
        public IActionResult DownloadCV(byte[] cv)
        {
            byte[] cvBytes = cv;

            // Provide the appropriate content type for the PDF file
            string contentType = "application/pdf";

            // Generate a unique file name for the PDF file
            string fileName = Guid.NewGuid().ToString() + ".pdf";

            // Get the file path for saving the PDF file on the server
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", fileName);

            // Save the byte array as a PDF file on the server
            System.IO.File.WriteAllBytes(filePath, cvBytes);

            // Create a new tab and open the PDF file in the browser
            return Content($"<script>window.open('{Url.Content("~/PDFs/" + fileName)}', '_blank');</script>", "text/html");
        }

        /// Interview functions
        /// These are functions that are used in emails and not in the code
        /// so even though they are unreferenced, they are certainly used
        public IActionResult Reject(int applicantID)
        {
            _service.UpdateGrade(applicantID, "Rejected");

            return View();
        }
        public IActionResult Accept(int applicantID)
        {
            return this.UpdateStatus(applicantID, "For HR Interview");
           // _service.ProceedTo(applicantID, "For HR Interview");
        }

        public IActionResult AcceptInterview(int applicantID)
        {
            _service.UpdateGrade(applicantID, "Passed");
            // _service.ProceedTo(applicantID, "For HR Interview");
            return View();
        }

        

    }
}
