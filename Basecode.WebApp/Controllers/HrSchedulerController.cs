using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HrSchedulerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
