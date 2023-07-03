﻿using Basecode.Services.Interfaces;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class JobPostingController : Controller
    {
        private readonly IJobOpeningService _service;

        public JobPostingController(IJobOpeningService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var jobOpenings = _service.RetrieveAll();
            return View("Index", jobOpenings);
        }

        [HttpGet]
        public IActionResult UpdateView(int id)
        {
            var JobOpening = _service.GetById(id);
            return View(JobOpening);
        }

        [HttpPost]
        public IActionResult Update(JobOpening JobOpening)
        {
            _service.Update(JobOpening);
            return RedirectToAction("Index");
        }
    }
}
