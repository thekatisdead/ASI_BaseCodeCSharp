using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Basecode.Test.Controllers
{
    public class HrHomepageControllerTests
    {
        private readonly HrHomepageController _controller;
        private readonly Mock<IJobOpeningService> _jobOpeningService;
        private readonly Mock<IApplicantListService> _applicantListService;
        private readonly Mock<IUserService> _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public HrHomepageControllerTests()
        {
            _jobOpeningService = new Mock<IJobOpeningService>();
            _applicantListService = new Mock<IApplicantListService>();
            _userService = new Mock<IUserService>();
            _controller = new HrHomepageController(_jobOpeningService.Object, _applicantListService.Object, _userManager, _userService.Object);
        }

        [Fact]
        public async Task Index_HasExistingUsername_ReturnsView()
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

            // Act
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "NonExistingUser")
                    }))
                }
            };

            // Act
            var result = _controller.Index("NonExistingUser");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var compositeViewModel = Assert.IsType<CompositeViewModel>(viewResult.Model);
            Assert.Equal(recentJobOpening, compositeViewModel.JobOpeningData);
            Assert.Equal(applicantsData, compositeViewModel.ApplicantsData);

            // Check that the ViewBag.Name is set correctly to "Guest" since the user is not found
            Assert.Equal("Guest", _controller.ViewBag.Name);
        }

        [Fact]
        public async Task Index_ReturnsViewWithGuestUsername_WhenUserNotFound()
        {
            // Arrange
            var recentJobOpening = new JobOpeningViewModel();
            var applicantsData = new ApplicantListViewModel();

            _jobOpeningService.Setup(s => s.GetMostRecentJobOpening()).Returns(recentJobOpening);
            _applicantListService.Setup(s => s.GetMostRecentApplicant()).Returns(applicantsData);

            // Set a dummy user name for testing
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "NonExistingUser")
                    }))
                }
            };

            // Act
            var result = _controller.Index("NonExistingUser");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var compositeViewModel = Assert.IsType<CompositeViewModel>(viewResult.Model);
            Assert.Equal(recentJobOpening, compositeViewModel.JobOpeningData);
            Assert.Equal(applicantsData, compositeViewModel.ApplicantsData);

            // Check that the ViewBag.Name is set correctly to "Guest" since the user is not found
            Assert.Equal("Guest", _controller.ViewBag.Name);
        }


    }
}
