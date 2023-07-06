using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Basecode.Test.Controllers
{
    public class ApplicantListControllerTests
    {
        private readonly ApplicantListController _controller;
        private readonly Mock<IApplicantListService> _fakeApplicantListService;

        public ApplicantListControllerTests()
        {
            _fakeApplicantListService = new Mock<IApplicantListService>();
            _controller = new ApplicantListController(_fakeApplicantListService.Object);
        }

        [Fact]
        public void Index_HasApplicants_ReturnsViewWithModel()
        {
            // Arrange
            var testData = new List<ApplicantListViewModel>
            {
                new ApplicantListViewModel { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 2 },
                new ApplicantListViewModel { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 1 },
                new ApplicantListViewModel { Id = 3, Firstname = "Bob", Lastname = "Johnson", JobApplied = 3 }
            };

            _fakeApplicantListService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Index_HasNoApplicants_ReturnsViewWithEmptyModel()
        {
            // Arrange
            var emptyTestData = new List<ApplicantListViewModel>();
            _fakeApplicantListService.Setup(s => s.RetrieveAll()).Returns(emptyTestData);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Model as List<ApplicantListViewModel>);
            Assert.Null(result.ViewName);
        }
    }
}
