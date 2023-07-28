using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserViewService _service;

        public UserManagementController(IUserViewService userService)
        {
            _service = userService;
        }

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
