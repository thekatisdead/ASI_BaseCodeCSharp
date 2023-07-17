using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class HrHomepageController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IActionResult Index(string Username)
        {
            // This part is unresolved, mainly because it requires a 'session'
            // This code just takes the username from the forms and uses it in our dashboard
            // In the actual program, this will be replaced by the first and last name
            // once we have defined our models
            _logger.Trace("HrHomepage Controller Accessed");
            ViewBag.Name = Username;
            return View();
        }
    }
}
