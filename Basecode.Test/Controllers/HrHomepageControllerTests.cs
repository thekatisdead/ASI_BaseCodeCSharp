using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class HrHomepageControllerTests
    {
        private readonly HrHomepageController _controller;
        private readonly Mock<IJobOpeningService> _jobOpeningService;
        private readonly Mock<IApplicantListService> _service;
       
        public HrHomepageControllerTests()
        {
            _jobOpeningService = new Mock<IJobOpeningService>();
            _service = new Mock<IApplicantListService>();
            _controller = new HrHomepageController(_jobOpeningService.Object, _service.Object);
        }

        [Fact]
        public void Index_HasExistingUsername_ReturnsView()
        {
            // Arrange
            // Set up the behavior for IJobOpeningService
            var username = "TestUsername";
            var recentJobOpening = new JobOpeningViewModel
            {
                Id = 1,
                Position = "Software Developer",
                JobType = "Full-Time",
                Salary = 60000,
                Hours = 40,
                Shift = "Day",
                Description = "Job description for testing",
            };
            
            // Set up the behavior for IApplicantListService
            var applicantsData = new ApplicantListViewModel
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Doe",
                Tracker = "Application Tracker",
                JobApplied = 2,
            };

            var data1 = _jobOpeningService.Setup(s => s.GetMostRecentJobOpening()).Returns(recentJobOpening);
            var data2 = _service.Setup(s => s.GetMostRecentApplicant()).Returns(applicantsData);


            //Add composite model
            var compositeModel = new CompositeViewModel
            {
                JobOpeningData = (JobOpeningViewModel) data1,
                ApplicantsData = (ApplicantListViewModel) data2
            };

            // Act
            var result = _controller.Index(username) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(compositeModel, result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Index_NoExistingUsername_ReturnsView()
        {
            // Arrange
            // Set up the behavior for IJobOpeningService with null recentJobOpening
            _jobOpeningService.Setup(s => s.GetMostRecentJobOpening()).Returns((JobOpeningViewModel)null);

            // Set up the behavior for IApplicantListService with null applicantsData
            _service.Setup(s => s.GetMostRecentApplicant()).Returns((ApplicantListViewModel)null);

            // Act
            var result = _controller.Index("TestUsername");

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}