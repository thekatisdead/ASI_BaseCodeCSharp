using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using Basecode.Data.Models;
namespace Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IJobOpeningService _jobOpeningService;
        private readonly IUserViewService _userService;

        public AdminController(IJobOpeningService jobOpeningService, IUserViewService userService)
        {
            _jobOpeningService= jobOpeningService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminJobListing()
        {
            var job = _jobOpeningService.RetrieveAll();
            return View(job);
        }
        public IActionResult UserManagement()
        {
            var users = _userService.RetrieveAll();
            return View(users);
        }
    }
}
