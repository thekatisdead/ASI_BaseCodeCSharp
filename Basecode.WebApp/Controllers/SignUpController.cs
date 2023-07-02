using Basecode.Main.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Basecode.Main.Controllers
{
    public class SignUpController : Controller
    {
        [Route("signup")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
