using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using NLog;
using Microsoft.AspNetCore.Identity;
using Basecode.Data.ViewModels;
using Basecode.Data.Models;
//using Microsoft.Graph.Beta.Models;


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

        /// <summary>
        /// Displays the RoleManagement view for managing user roles.
        /// </summary>
        /// <returns>The RoleManagement view.</returns>
        public IActionResult RoleManagement()
        {
            return View();
        }

        /// <summary>
        /// Displays the CreateRole view for creating new roles.
        /// </summary>
        /// <returns>The CreateRole view.</returns>
        public IActionResult CreateRole()
        {
            return View("RoleManagement/CreateRole");
        }

        /// <summary>
        /// Handles the POST request to create a new role in the application.
        /// </summary>
        /// <param name="createRoleViewModel">The view model containing information for creating a new role.</param>
        /// <returns>A redirect to the Admin's index page if the role is created successfully; otherwise, returns the CreateRole view.</returns>
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

        /// <summary>
        /// Displays the AdminJobListing view to list all job openings managed by the admin.
        /// </summary>
        /// <returns>The AdminJobListing view with the list of job openings.</returns>
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

        /// <summary>
        /// Displays the UpdateJobAdmin view for updating a specific job opening managed by the admin.
        /// </summary>
        /// <param name="id">The ID of the job opening to be updated.</param>
        /// <returns>The UpdateJobAdmin view with the details of the job opening.</returns>
        public IActionResult UpdateJobAdmin(int id)
        {
            _logger.Trace("UpdateJobAdmin action called");
            var data = _jobOpeningService.GetById(id);
            return View(data);
        }

        /// <summary>
        /// Handles the POST request to update a job opening managed by the admin.
        /// </summary>
        /// <param name="jobOpening">The updated job opening object.</param>
        /// <returns>A redirect to the AdminJobListing page if the job opening is updated successfully; otherwise, returns the UpdateJobAdmin view.</returns>
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

        /// <summary>
        /// Displays the DeleteJobAdmin view for confirming the deletion of a job opening managed by the admin.
        /// </summary>
        /// <param name="id">The ID of the job opening to be deleted.</param>
        /// <returns>The DeleteJobAdmin view with the details of the job opening.</returns>
        public IActionResult DeleteJobAdmin(int id)
        {
            _logger.Trace("DeleteJobAdmin action called");
            var data = _jobOpeningService.GetById(id);
            return View(data);
        }

        /// <summary>
        /// Handles the POST request to delete a job opening managed by the admin.
        /// </summary>
        /// <param name="id">The ID of the job opening to be deleted.</param>
        /// <returns>A redirect to the AdminJobListing page after successful deletion; otherwise, returns the AdminJobListing view.</returns>
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

        /// <summary>
        /// Displays the UserManagement view to list all users managed by the admin.
        /// </summary>
        /// <returns>The UserManagement view with the list of users.</returns>
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

        /// <summary>
        /// Displays the UpdateUser view for updating a specific user managed by the admin.
        /// </summary>
        /// <param name="id">The ID of the user to be updated.</param>
        /// <returns>The UpdateUser view with the details of the user.</returns>
        public IActionResult UpdateUser(string id)
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

        /// <summary>
        /// Handles the POST request to update a user managed by the admin.
        /// </summary>
        /// <param name="user">The updated user object.</param>
        /// <returns>A redirect to the UserManagement page if the user account is updated successfully; otherwise, returns the UpdateUser view.</returns>
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
                return RedirectToAction("UpdateUser", new { username = user.Username });
            }
        }

        // <summary>
        /// Handles the request to delete a user managed by the admin.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>A redirect to the UserManagement page after successful deletion; otherwise, returns the UserManagement view.</returns>
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
