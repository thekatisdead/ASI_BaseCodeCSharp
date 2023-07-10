using Microsoft.AspNetCore.Mvc;
using Basecode.Data.Repositories;
using Basecode.Data.Models;

namespace Basecode.WebApp.Controllers
{
    public class CurrentHiresController : Controller
    {
        private readonly CurrentHiresRepository _repository;

        public CurrentHiresController(CurrentHiresRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateInfo(int applicantID, int jobID)
        {
            _repository.AddHire(applicantID, jobID);
 
            return View();
        }
    }
}
