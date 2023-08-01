using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using NLog;
using Microsoft.AspNetCore.Identity;
using Basecode.Data.ViewModels;
using Basecode.Data.Models;
using Microsoft.Graph.Beta.Models;

namespace Basecode.WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IJobOpeningService _jobOpeningService;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdminService _service;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public AdminController(IJobOpeningService jobOpeningService, IUserService userService, RoleManager<IdentityRole> roleManager, IAdminService service)
        {
            _jobOpeningService = jobOpeningService;
            _userService = userService;
            _roleManager = roleManager;
            _service = service;
            //RoleManager<IdentityRole> roleManager, IAdminService service //enable this if you want to add new role
        }
        public IActionResult Index()
        {
            _logger.Trace("Admin Controller Accessed");
            return View();
        }

        public IActionResult RoleManagement()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View("RoleManagement/CreateRole");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {

                IdentityResult result = await _service.CreateRole(createRoleViewModel.RoleName);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View();
        }

        public IActionResult AdminJobListing()
        {
            try
            {
                var jobs = _jobOpeningService.RetrieveAll();

                // Log the number of retrieved job openings
                _logger.Info("Retrieved {jobCount} job openings for admin listing", jobs.Count);

                return View(jobs);
            }
            catch (System.Exception ex)
            {
                // Log the error using a logger
                _logger.Error(ex, "Error occurred while retrieving admin job listings: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while retrieving the job openings.");
            }
        }

        public IActionResult UpdateJobAdmin(int id)
        {
            _logger.Trace("UpdateJobAdmin action called");
            var data = _jobOpeningService.GetById(id);
            return View(data);
        }

        public IActionResult UpdateAdminJob(JobOpening jobOpening)
        {
            _logger.Info("UpdateAdminJob action called");
            try
            {
                _jobOpeningService.Update(jobOpening);
                _logger.Info("Job opening updated successfully.");
                return RedirectToAction("AdminJobListing", "Admin");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating job opening.");
                return RedirectToAction("AdminJobListing", new { id = jobOpening.Id });
            }
        }

        public IActionResult DeleteJobAdmin(int id)
        {
            _logger.Trace("DeleteJobAdmin action called");
            var data = _jobOpeningService.GetById(id);
            return View(data);
        }

        public IActionResult DeleteAdminJob(int id)
        {
            _logger.Info("DeleteAdminJob action called");
            try
            {
                _jobOpeningService.Delete(id);
                _logger.Info("Job opening deleted successfully.");
                return RedirectToAction("AdminJobListing", "Admin");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting job opening.");
                return RedirectToAction("AdminJobListing", "Admin");
            }
        }

        public IActionResult UserManagement()
        {
            try
            {
                var users = _userService.RetrieveAll();

                // Log the number of retrieved users
                _logger.Info("Retrieved {userCount} users for user management", users.Count);

                return View(users);
            }
            catch (System.Exception ex)
            {
                // Log the error using a logger
                _logger.Error(ex, "Error occurred while retrieving users for user management: {errorMessage}", ex.Message);

                // You can customize the error handling based on your application's requirements
                // For example, you can return a specific error view or redirect to an error page.
                return BadRequest("An error occurred while retrieving users.");
            }
        }

        public IActionResult Update(string id)
        {
            try
            {
                var user = _userService.FindByUsername(id);
                if (user == null)
                {
                    _logger.Error("User not found.");
                    return RedirectToAction("UserManagement", "Admin");
                }

                return View(user);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving user for update: {errorMessage}", ex.Message);
                return RedirectToAction("UserManagement", "Admin");
            }
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            _logger.Info("Update action called");
            try
            {
                _userService.Update(user);
                _logger.Info("User account updated successfully.");
                return RedirectToAction("UserManagement", "Admin");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating user account.");
                return RedirectToAction("Update", new { id = user.Id });
            }
        }

        public IActionResult Delete(string id)
        {
            _logger.Info("Delete action called");
            try
            {
                var data = _userService.FindByUsername(id);
                _userService.Delete(data);
                _logger.Info("User deleted successfully.");
                return RedirectToAction("UserManagement", "Admin");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting user.");
                return RedirectToAction("UserManagement", "Admin");
            }
        }

    }
}
