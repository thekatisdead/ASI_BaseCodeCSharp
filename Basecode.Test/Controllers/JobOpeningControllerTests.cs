using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class JobOpeningControllerTests
    {
        private readonly JobOpeningController _controller;
        private readonly Mock<IJobOpeningService> _mockJobOpeningService;
        public JobOpeningControllerTests()
        {
            _mockJobOpeningService = new Mock<IJobOpeningService>();
            _controller = new JobOpeningController(_mockJobOpeningService.Object);
        }

        [Fact]
        public void JobList_HasJobOpening_ReturnsAllJobOpeningViewModel()
        {
            // Arrange
            var testData = new List<JobOpeningViewModel>
            {
                new JobOpeningViewModel { Id = 1, Position = "Software Developer", JobType = "Full Time", Salary = 123, Hours = 3, Shift = "Morning", Description = "Hello Wordld" },
                new JobOpeningViewModel { Id = 2, Position = "Software Tester", JobType = "Full Time", Salary = 465, Hours = 3, Shift = "Graveyard", Description = "Hello" },
                new JobOpeningViewModel { Id = 3, Position = "Software Architect", JobType = "Full Time", Salary = 789, Hours = 3, Shift = "Evening", Description = "Hello" }
            };

            _mockJobOpeningService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.JobList() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
        }


        [Fact]
        public void JobList_NoJobOpeningFound_ReturnsEmpty()
        {
            // Arrange
            var testData = new List<JobOpeningViewModel>();

            _mockJobOpeningService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.JobList() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
            Assert.Empty(testData);
        }

        [Fact]
        public void UpdateJob_HasJobOpening_ReturnsJobOpeningViewModel()
        {
            //Arrange
            int jobId = It.IsAny<int>();

            var testData = new JobOpeningViewModel
            {
                Id = 1,
                Position = "Software Developer",
                JobType = "Full Time",
                Salary = 123,
                Hours = 3,
                Shift = "Morning",
                Description = "Hello Wordld"
            };

            _mockJobOpeningService.Setup(s => s.GetById(jobId)).Returns(testData);

            // Act
            var result = _controller.UpdateJob(jobId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void UpdateJob_NoJobOpeningFound_ReturnsJobOpeningViewModel()
        {
            //Arrange
            int jobId = It.IsAny<int>();

            var testData = new JobOpeningViewModel();

            _mockJobOpeningService.Setup(s => s.GetById(jobId)).Returns(testData);

            // Act
            var result = _controller.UpdateJob(jobId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
        }


        [Fact]
        public void Update_HasJobOpening_ReturnsRedirectToAction()
        {
            // Arrange
            var jobOpening = new JobOpening
            {
                Id = 1,
                Position = "Software Developer",
                JobType = "Full Time",
                Salary = 123,
                Hours = 3,
                Shift = "Morning",
                Description = "Hello Wordld",
                CreatedBy = System.Environment.UserName,
                UpdatedBy = System.Environment.UserName,
            };

            _mockJobOpeningService.Setup(s => s.Update(jobOpening));
            // Act
            var result = _controller.Update(jobOpening);

            // Assert
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("AdminJobListing", redirectToActionResult.ActionName); // Ensure that the action name is "JobList"
        }

        [Fact]
        public void Update_NoJobOpening_ReturnsRedirectToAction()
        {
            // Arrange
            var jobOpening = new JobOpening();

            _mockJobOpeningService.Setup(s => s.Update(jobOpening));

            // Act
            var result = _controller.Update(jobOpening);

            // Assert
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("AdminJobListing", redirectToActionResult.ActionName); // Ensure that the action name is "JobList"
        }


        [Fact]
        public void DeleteJob_HasJobOpening_ReturnJobOpeningViewModel()
        {
            //Arrange
            int jobId = It.IsAny<int>();

            var testData = new JobOpeningViewModel
            {
                Id = 1,
                Position = "Software Developer",
                JobType = "Full Time",
                Salary = 123,
                Hours = 3,
                Shift = "Morning",
                Description = "Hello Wordld"
            };

            _mockJobOpeningService.Setup(s => s.GetById(jobId)).Returns(testData);

            // Act
            var result = _controller.DeleteJob(jobId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void DeleteJob_NoJobOpening_ReturnJobOpeningViewModel()
        {
            //Arrange
            int jobId = It.IsAny<int>();

            var testData = new JobOpeningViewModel();

            _mockJobOpeningService.Setup(s => s.GetById(jobId)).Returns(testData);

            // Act
            var result = _controller.DeleteJob(jobId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Delete_HasJobOpening_ReturnsRedirectToAction()
        {
            // Arrange
            int jobId = It.IsAny<int>();

            _mockJobOpeningService.Setup(s => s.Delete(jobId));

            // Act
            var result = _controller.Delete(jobId);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("AdminJobListing", redirectToActionResult.ActionName); // Ensure that the action name is "JobList"
        }

        [Fact]
        public void Delete_NoJobOpening_ReturnsRedirectToAction()
        {
            // Arrange
            int jobId = It.IsAny<int>();

            _mockJobOpeningService.Setup(s => s.Delete(jobId));

            // Act
            var result = _controller.Delete(jobId);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("AdminJobListing", redirectToActionResult.ActionName); // Ensure that the action name is "JobList"
        }
    }
}
