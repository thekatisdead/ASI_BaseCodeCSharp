using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NuGet.Protocol;
using System;

namespace Basecode.WebApp.Controllers
{
    public class ApplicationTrackingController : Controller
    {
        [Route("applicationtracking")]
        public IActionResult Index(int ApplicantId)
        {

            var db = ApplicationTrackingModel.Find(ApplicantId);
            ViewBag.Id = ApplicantId;
            return View(db);
        }
    }
}
