using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class LoginController : Controller
    {
        [Route("login")]
        public IActionResult Index()
        {
            return View();
        }

        //public Task<IActionResult> Login([Bind("Email,Password")] LoginModel loginModel)
        //{
        //    return Task.FromResult<IActionResult>(RedirectToAction("Login", "Dashboard", loginModel));
        //}
    }
}
