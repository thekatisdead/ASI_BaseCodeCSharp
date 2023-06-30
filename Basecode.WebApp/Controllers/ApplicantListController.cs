using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
