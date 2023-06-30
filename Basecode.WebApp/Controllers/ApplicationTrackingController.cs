using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicationTrackingController : Controller
    {
        [Route("applicationtracking")]
        public IActionResult Index(int ApplicantId)
        {
            ViewBag.Id = ApplicantId;
            return View();
        }
    }
}
