using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        private readonly IApplicantListService _service;

        public ApplicantListController(IApplicantListService service)
        {
            _service = service;
        }

        /// <summary>
        /// Displays the index view for the applicant list.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }
    }
}
