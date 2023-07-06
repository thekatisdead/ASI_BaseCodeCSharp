using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Route("login")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Login method for the user
        /// </summary>
        /// <param name="loginModel">Model containing the Username and Password of the user</param>
        /// <returns>
        /// If successful, redirects to the index page of the HR Home Page
        /// and if not, redirects back to the login page.
        /// </returns>
        public Task<IActionResult> Login([Bind("Username,Password")] LoginModel loginModel)
        {
            var login = _loginService.GetByUsername(loginModel.Username);

            if (login == null)
            {
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }

            if (login.Username != loginModel.Username)
            {
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }

            if (login.Password != loginModel.Password)
            {
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }

            return Task.FromResult<IActionResult>(RedirectToAction("Index", "HrHomePage"));
        }



    }
}
