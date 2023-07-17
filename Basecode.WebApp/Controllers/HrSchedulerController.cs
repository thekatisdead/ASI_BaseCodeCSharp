using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class HrSchedulerController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public IActionResult Index()
        {
            _logger.Trace("HrScheduler Controller Accessed");
            return View();
        }
    }
}
