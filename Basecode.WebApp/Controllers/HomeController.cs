using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Views.Dashboard
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
