//using Basecode.Data.ViewModels;
//using Basecode.Services.Interfaces;
//using Basecode.WebApp.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Moq;

//namespace Basecode.Test.Controllers
//{
//    public class ApplicantHomepageControllerTests
//    {
//        private readonly ApplicantHomepageController _controller;
//        private readonly Mock<IJobOpeningService> _mockJobOpeningService;
//        private readonly Mock<IApplicantListService> _mockApplicantListService; 

//        public ApplicantHomepageControllerTests()
//        {
//            _mockJobOpeningService = new Mock<IJobOpeningService>();
//            _controller = new ApplicantHomepageController(_mockJobOpeningService.Object,_mockApplicantListService.Object);
//        }

//        [Fact]
//        public void Index_ReturnsView()
//        {
//            // Act
//            var result = _controller.Index();

//            // Assert
//            Assert.NotNull(result);
//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.Null(viewResult.ViewName);
//        }

//        [Fact]
//        public void JobListing_HasJobOpenings_ReturnsViewWithJobOpenings()
//        {
//            // Arrange
//            var testData = new List<JobOpeningViewModel>
//            {
//                new JobOpeningViewModel
//                {
//                    Id = 1,
//                    Position = "Software Engineer",
//                    JobType = "Full Time",
//                    Salary = 5000,
//                    Hours = 8,
//                    Shift = "Morning",
//                    Description = "Job opening for a software engineer."
//                },
//                new JobOpeningViewModel
//                {
//                    Id = 2,
//                    Position = "Data Analyst",
//                    JobType = "Part Time",
//                    Salary = 3000,
//                    Hours = 6,
//                    Shift = "Night",
//                    Description = "Job opening for a data analyst."
//                }
//            };

//            _mockJobOpeningService.Setup(s => s.RetrieveAll()).Returns(testData);

//            // Act
//            var result = _controller.JobListing();

//            // Assert
//            Assert.NotNull(result);
//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.Null(viewResult.ViewName);
//            Assert.Equal(testData, viewResult.Model);
//        }

//        [Fact]
//        public void JobListing_HasNoJobOpenings_ReturnsViewWithEmptyJobList()
//        {
//            // Arrange
//            var testData = new List<JobOpeningViewModel>();

//            _mockJobOpeningService.Setup(s => s.RetrieveAll()).Returns(testData);

//            // Act
//            var result = _controller.JobListing();

//            // Assert
//            Assert.NotNull(result);
//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.Null(viewResult.ViewName);
//            Assert.Empty(viewResult.Model as List<JobOpeningViewModel>);
//        }
//    }
//}
