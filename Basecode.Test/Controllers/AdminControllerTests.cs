using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class AdminControllerTests
    {
        private readonly AdminController _controller;
        private readonly Mock<IJobOpeningService> _mockJobOpeningService;
        private readonly Mock<IUserViewService> _mockUserViewService;

        public AdminControllerTests()
        {
            _mockJobOpeningService = new Mock<IJobOpeningService>();
            _mockUserViewService = new Mock<IUserViewService>();
            _controller = new AdminController(_mockJobOpeningService.Object, _mockUserViewService.Object);
        }

        [Fact]
        public void Index_ReturnsView()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public void AdminJobListing_HasJobOpenings_ReturnsViewWithJobOpenings()
        {
            // Arrange
            var testData = new List<JobOpeningViewModel>
            {
                new JobOpeningViewModel
                {
                    Id = 1,
                    Position = "Software Engineer",
                    JobType = "Full Time",
                    Salary = 5000,
                    Hours = 8,
                    Shift = "Morning",
                    Description = "Job opening for a software engineer."
                },
                new JobOpeningViewModel
                {
                    Id = 2,
                    Position = "Data Analyst",
                    JobType = "Part Time",
                    Salary = 3000,
                    Hours = 6,
                    Shift = "Night",
                    Description = "Job opening for a data analyst."
                }
            };

            _mockJobOpeningService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.AdminJobListing();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.Equal(testData, viewResult.Model);
        }

        [Fact]
        public void AdminJobListing_HasNoJobOpenings_ReturnsViewWithNullModel()
        {
            //Act
            var testdata = new List<JobOpeningViewModel>();

            // Arrange
            _mockJobOpeningService.Setup(s => s.RetrieveAll()).Returns(testdata);

            // Act
            var result = _controller.AdminJobListing() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testdata, result.Model);
            Assert.Null(result.ViewName);
            Assert.Empty(testdata);
        }

        [Fact]
        public void UserManagement_HasUsers_ReturnsViewWithUsers()
        {
            // Arrange
            var testData = new List<UserViewModel>
            {
                new UserViewModel
                {
                    UserName = "johndoe",
                    Password = "king",
                    ConfirmPassword = "king",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "johndoe@example.com",
                    Address = "123 Street, City",
                    RoleName = "Applicant"
                },
                new UserViewModel
                {
                    UserName = "janedoe",
                    Password = "queen",
                    ConfirmPassword = "queen",
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "janedoe@example.com",
                    Address = "456 Street, City",
                    RoleName = "Applicant"
                }
            };

            _mockUserViewService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.UserManagement();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.Equal(testData, viewResult.Model);
        }

        [Fact]
        public void UserManagement_HasNoUsers_ReturnsViewWithNullModel()
        {
            // Act 
            var testData = new List<UserViewModel>();

            // Arrange
            _mockUserViewService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.UserManagement() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
            Assert.Empty(testData);
        }
    }
}