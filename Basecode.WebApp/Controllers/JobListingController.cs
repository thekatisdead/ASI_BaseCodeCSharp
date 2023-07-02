using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class JobListingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
