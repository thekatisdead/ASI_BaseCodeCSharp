using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        private readonly IApplicantListService _service;
        private readonly IEmailSenderService _email;
        private readonly CurrentHiresRepository _repository;
        private readonly UserRepository _users;
        private readonly JobOpeningRepository _job;

        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public ApplicantListController(IApplicantListService service, IEmailSenderService email, JobOpeningRepository job, UserRepository users, CurrentHiresRepository repository)
        {
            _service = service;
            _email = email;
            _job = job;
            _users = users;
            _repository = repository;
        }

        /// <summary>
        /// Displays the index view for the applicant list.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            this.UpdateStatus(2,"For Hire");
            var data = _service.RetrieveAll();
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
            //var temp_id = _job.GetById(3);
            
            var _fullName = data.Lastname + " " + data.Firstname;
            var job = _job.GetById(data.JobApplied);
            Logger.Trace(job.HR);
            var _receiver = _users.FindById((job.HR).ToString());

            // sends an update whenever the applicant status is changed
            _email.SendEmailOnUpdateApplicantStatus(_receiver.EmailAddress,_fullName,data.Tracker,status);
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
                
                _email.SendEmailHRApplicationDecision(_receiver.EmailAddress,applicantID,_fullName,job.Position);
            }
                    

            var _allData = _service.RetrieveAll();
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int applicantID)
        {
            _service.UpdateStatus(applicantID, "Rejected");

            return View();
        }
        public IActionResult Accept(int applicantID)
        {
            return this.UpdateStatus(applicantID, "For HR Interview");
           // _service.ProceedTo(applicantID, "For HR Interview");
        }

        /// Interview functions
        /// 

    }
}
