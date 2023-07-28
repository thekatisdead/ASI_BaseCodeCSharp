using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Repositories;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class CurrentHiresController : Controller
    {
        private readonly ICurrentHiresService _currentHiresService;


        public CurrentHiresController(ICurrentHiresService currentHiresService)
        {
            _currentHiresService = currentHiresService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateInfo(int applicantID, int jobID)
        {
            _currentHiresService.AddHire(applicantID, jobID);
 
            return View();
        }
    }
}
