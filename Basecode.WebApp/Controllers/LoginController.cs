using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private ErrorHandling.LogContent _log = new ErrorHandling.LogContent();
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            _logger.Trace("Login Controller Accessed");
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
                _log.ErrorCode = "null";
                _log.Time = DateTime.Now;
                _log.Result = true;
                _log.Message = "fields empty";
                _logger.Trace(ErrorHandling.SetLog(_log));
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }

            if (login.Username != loginModel.Username)
            {
                _log.ErrorCode = "username";
                _log.Time = DateTime.Now;
                _log.Result = true;
                _log.Message = "username not matched.";
                _logger.Trace(ErrorHandling.SetLog(_log));
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }

            if (login.Password != loginModel.Password)
            {
                _log.ErrorCode = "password";
                _log.Time = DateTime.Now;
                _log.Result = true;
                _log.Message = "password not matched.";
                _logger.Trace(ErrorHandling.SetLog(_log));
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }

            return Task.FromResult<IActionResult>(RedirectToAction("Index", "HrHomePage"));
        }



    }
}
