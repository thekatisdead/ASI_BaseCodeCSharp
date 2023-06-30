using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HrHomepageController : Controller
    {
        public IActionResult Index(string Username)
        {
            // This part is unresolved, mainly because it requires a 'session'
            // This code just takes the username from the forms and uses it in our dashboard
            // In the actual program, this will be replaced by the first and last name
            // once we have defined our models
            ViewBag.Name = Username;
            return View();
        }
    }
}
