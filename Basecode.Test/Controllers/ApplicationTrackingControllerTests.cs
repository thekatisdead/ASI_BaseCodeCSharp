using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Basecode.Test.Controllers
{
    public class ApplicationTrackingControllerTests
    {
        private readonly ApplicationTrackingController _controller;
        private readonly ApplicationTrackingRepository _applicationTrackingRepository;
        private readonly JobOpeningRepository _jobOpeningRepository;
        private readonly BasecodeContext _context;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ApplicationTrackingControllerTests()
        {
            var options = new DbContextOptionsBuilder<BasecodeContext>()
                .UseInMemoryDatabase(databaseName: "HRAutomationSystem")
                .Options;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _context = new BasecodeContext(options);
            _applicationTrackingRepository = new ApplicationTrackingRepository(_context);
            _jobOpeningRepository = new JobOpeningRepository(_mockUnitOfWork.Object, _context);
            _controller = new ApplicationTrackingController(_applicationTrackingRepository, _jobOpeningRepository);
        }

        [Fact]
        public void Index_ApplicationTrackingExists_ReturnsViewWithApplicationTracking()
        {
            // Arrange
            var applicantId = 1;
            var applicationTrackingData = new Applicant
            {
                Id = applicantId,
                Firstname = "Aima",
                Lastname = "Gudgurl",
                EmailAddress = "aima@example.com",
                JobApplied = 1,
                Tracker = "Processing",
                Grading = "Ongoing"
            };

            var jobOpeningData = new JobOpening
            {
                Id = 1,
                Position = "Software Engineer",
                JobType = "Full Time",
                Salary = 5000,
                Hours = 8,
                Shift = "Morning",
                Description = "Job opening for a software engineer."
            };

            _context.Applicant.Add(applicationTrackingData);
            _context.JobOpening.Add(jobOpeningData);
            _context.SaveChanges();

            // Act
            var result = _controller.Index(applicantId) as ViewResult;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.Equal(applicationTrackingData, viewResult.Model);
            Assert.Equal(jobOpeningData, viewResult.ViewData["jobOpening"]);
        }

        // Other test methods
    }
}