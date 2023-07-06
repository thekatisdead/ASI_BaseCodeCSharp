using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        [Route("publicapplicationform")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("page2-publicapplicationform")]
        public IActionResult IndexTwo()
        {
            return View();
        }
    }
}
