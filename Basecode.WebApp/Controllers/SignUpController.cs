using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ISignUpService _service;

        public SignUpController(ISignUpService service)
        {
            _service = service;
        }

        [Route("signup")]
        public IActionResult Index()
        {
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
            _service.CreateAccount(newAccount);
            TempData["SuccessMessage"] = "Successfully created an account! You may proceed to log in to our system.";
            return RedirectToAction("Index");
        }
    }
}
