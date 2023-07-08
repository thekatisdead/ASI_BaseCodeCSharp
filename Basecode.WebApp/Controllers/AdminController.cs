using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
