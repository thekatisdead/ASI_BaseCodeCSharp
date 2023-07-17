using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ISignUpService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SignUpController(ISignUpService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            _logger.Trace("SignUp Controller Accessed");
            return View();
        }

        /// <summary>
        /// Creates a new user account using the provided user management data and redirects to the index view.
        /// </summary>
        /// <param name="newAccount">The needed details for creating the account.</param>
        /// <returns>The index view.</returns>
        [HttpPost]
        public IActionResult CreateAccount(SignUpViewModel newAccount)
        {
            _logger.Info("CreateAccount action called");

            try
            {
                _service.CreateAccount(newAccount);
                TempData["SuccessMessage"] = "Successfully created an account! You may proceed to log in to our system.";
                _logger.Info("Account created successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating an account.");
            }
            return RedirectToAction("Index");
        }
    }
}
