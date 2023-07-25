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
        private readonly Mock<IApplicantListService> _applicantListService;
       
        public HrHomepageControllerTests()
        {
            _jobOpeningService = new Mock<IJobOpeningService>();
            _applicantListService = new Mock<IApplicantListService>();
            _controller = new HrHomepageController(_jobOpeningService.Object, _applicantListService.Object);
        }

        [Fact]
        public void Index_HasExistingUsername_ReturnsView()
        {
            // Arrange
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
            
            var applicantsData = new ApplicantListViewModel
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Doe",
                Tracker = "Application Tracker",
                JobApplied = 2,
            };

            _jobOpeningService.Setup(s => s.GetMostRecentJobOpening()).Returns(recentJobOpening);
            _applicantListService.Setup(s => s.GetMostRecentApplicant()).Returns(applicantsData);

            //Add composite model
            var compositeModel = new CompositeViewModel
            {
                JobOpeningData = recentJobOpening,
                ApplicantsData = applicantsData
            };

            // Act
            var result = _controller.Index("TestUsername");

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            // Check that the view model passed to the view is of type CompositeViewModel
            var viewModel = Assert.IsType<CompositeViewModel>(viewResult.Model);
            Assert.Equal(recentJobOpening, viewModel.JobOpeningData);
            Assert.Equal(applicantsData, viewModel.ApplicantsData);

            // Check that the ViewBag.Name is set correctly
            Assert.Equal("TestUsername", _controller.ViewBag.Name);
        }

        [Fact]
        public void Index_NoExistingUsername_ReturnsView()
        {
            // Arrange
            var jobOpening = new JobOpeningViewModel();
            var applicantlist = new ApplicantListViewModel();

            // Set up the behavior for IJobOpeningService with null recentJobOpening
            _jobOpeningService.Setup(s => s.GetMostRecentJobOpening()).Returns(jobOpening);

            // Set up the behavior for IApplicantListService with null applicantsData
            _applicantListService.Setup(s => s.GetMostRecentApplicant()).Returns(applicantlist);

            //Add composite model
            var compositeModel = new CompositeViewModel
            {
                JobOpeningData = jobOpening,
                ApplicantsData = applicantlist
            };

            // Act
            var result = _controller.Index("");

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            // Check that the view model passed to the view is of type CompositeViewModel
            var viewModel = Assert.IsType<CompositeViewModel>(viewResult.Model);

            // Check that the ViewBag.Name is set correctly
            Assert.Equal("", _controller.ViewBag.Name);
        }
    }
}