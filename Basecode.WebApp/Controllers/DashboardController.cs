using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private static NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public IActionResult Index()
        {
            _logger.Trace("Dashboard Controller Accessed");
            return View();
        }
    }
}
