using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class RequirementsController : Controller
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IApplicantListService _service;

        public ActionResult Index()
        {
            _logger.Trace("Login Controller Accessed");

            var applicantsData = _service.GetMostRecentApplicantForRequirements();

            return View(applicantsData);
        }
    }
}
