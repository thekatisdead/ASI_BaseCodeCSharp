using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserViewService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserManagementController(IUserViewService userService)
        {
            _service = userService;
        }

        public IActionResult UpdateUser(int id)
        {
            var data = _service.GetUserById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(SignUp user)
        {
            _logger.Info("Update action called");
            try
            {
                _service.UpdateUser(user);
                _logger.Info("User account updated successfully.");
                return RedirectToAction("UserManagement");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating user account.");
                return RedirectToAction("UpdateUser", new { id = user.Id });
            }
        }
        public IActionResult DeleteUser(int id)
        {
            var data = _service.GetUserById(id);
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            _logger.Info("Delete action called");
            try
            {
                _service.DeleteUser(id);
                _logger.Info("User account deleted successfully.");
                return RedirectToAction("UserManagement");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting user account.");
                return RedirectToAction("UserManagement");
            }
        }
    }
}
