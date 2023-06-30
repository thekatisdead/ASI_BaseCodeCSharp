using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
