using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Views.Dashboard
{
    public class HomeController : Controller
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public IActionResult Index()
        {
            Logger.Trace("Home Controller Accessed");
            return View();
        }
    }
}
