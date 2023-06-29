using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicationTrackingController : Controller
    {
        [Route("applicationtracking")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
