using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantHomepageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       public IActionResult JobListing() 
        { 
            return View();  
        }
    }
}
