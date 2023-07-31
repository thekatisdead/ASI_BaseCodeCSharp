using AutoMapper;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserManagementController(IUserService userService)
        {
            _service = userService;
        }

        public IActionResult UpdateUser(string id)
        {
            var data = _service.FindById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(User user)
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
        public IActionResult DeleteUser(string id)
        {
            var data = _service.FindById(id);
            return View(data);
        }

        public IActionResult Delete(string id)
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
