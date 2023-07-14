using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class ApplicantListControllerTests
    {
        private readonly ApplicantListController _controller;
        private readonly Mock<IApplicantListService> _fakeApplicantListService;
        private readonly Mock<CurrentHiresRepository> _fakeCurrentHiresRepository;

        public ApplicantListControllerTests()
        {
            _fakeApplicantListService = new Mock<IApplicantListService>();
            _fakeCurrentHiresRepository = new Mock<CurrentHiresRepository>();
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

        //[Fact]
        //public void UpdateStatus_ValidApplicantIdAndStatus_ReturnsViewWithUpdatedModel()
        //{
        //    // Arrange
        //    var applicantId = 1;
        //    var status = "Hired";

        //    var applicantData = new List<ApplicantListViewModel>
        //    {
        //        new ApplicantListViewModel { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 2 },
        //        new ApplicantListViewModel { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 1 },
        //        new ApplicantListViewModel { Id = 3, Firstname = "Bob", Lastname = "Johnson", JobApplied = 3 }
        //    };

        //    _fakeApplicantListService.Setup(s => s.RetrieveAll()).Returns(applicantData);

        //    // Act
        //    var result = _controller.UpdateStatus(applicantId, status) as ViewResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<ApplicantListViewModel>>(result.Model);
        //    var updatedApplicant = model.FirstOrDefault(a => a.Id == applicantId);
        //    Assert.NotNull(updatedApplicant);
        //    Assert.Equal(status, updatedApplicant.Tracker);
        //    _fakeCurrentHiresRepository.Verify(r => r.AddHire(applicantId, It.IsAny<int>()), Times.Once);
        //}

        //[Fact]
        //public void UpdateStatus_InvalidApplicantId_ReturnsNotFound()
        //{
        //    // Arrange
        //    var applicantId = 999;
        //    var status = "Hired";

        //    var applicantData = new List<ApplicantListViewModel>
        //    {
        //        new ApplicantListViewModel { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 2 },
        //        new ApplicantListViewModel { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 1 },
        //        new ApplicantListViewModel { Id = 3, Firstname = "Bob", Lastname = "Johnson", JobApplied = 3 }
        //    };

        //    _fakeApplicantListService.Setup(s => s.RetrieveAll()).Returns(applicantData);

        //    // Act
        //    var result = _controller.UpdateStatus(applicantId, status);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<NotFoundResult>(result);
        //    _fakeCurrentHiresRepository.Verify(r => r.AddHire(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        //}
    }
}