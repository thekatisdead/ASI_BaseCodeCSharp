using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Basecode.Services.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserViewService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public IActionResult UpdateUser(int id)
        {
            var data = _service.GetUserById(id);
            return View(data);
        }

        public IActionResult DeleteUser(int id)
        {
            var data = _service.GetUserById(id);
            return View(data);
        }
    }
}
