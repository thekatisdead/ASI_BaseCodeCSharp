using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HrHomepageController : Controller
    {
        public IActionResult Index(string Username)
        {
            ViewBag.Name = Username;
            return View();
        }
    }
}
