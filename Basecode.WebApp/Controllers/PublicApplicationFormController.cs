using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class PublicApplicationFormController : Controller
    {
        [Route("public-application-form")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("page2-public-application-form")]
        public IActionResult IndexTwo()
        {
            return View();
        }

        [Route("page3-public-application-form")]
        public IActionResult IndexThree()
        {
            return View();
        }
    }
}
