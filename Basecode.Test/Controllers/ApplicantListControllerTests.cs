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
        private readonly Mock<ITeamsService> _fakeTeamsService;
        private readonly Mock<IEmailSenderService> _fakeEmailSenderService;
        private readonly Mock<CurrentHiresRepository> _fakeCurrentHiresRepository;
        private readonly Mock<JobOpeningRepository> _fakeJobOpeningRepository;
        private readonly Mock<UserRepository> _fakeUserRepository;
        private readonly Mock<IPublicApplicationFormService> _fakePublicApplicationFormService;

        public ApplicantListControllerTests()
        {
            _fakeApplicantListService = new Mock<IApplicantListService>();
            _fakePublicApplicationFormService = new Mock<IPublicApplicationFormService>();
            _fakeCurrentHiresRepository = new Mock<CurrentHiresRepository>();
            _fakeTeamsService = new Mock<ITeamsService>();
            _fakeEmailSenderService = new Mock<IEmailSenderService>();
            _fakeJobOpeningRepository = new Mock<JobOpeningRepository>();
            _fakeUserRepository = new Mock<UserRepository>();
            _controller = new ApplicantListController(_fakeApplicantListService.Object, _fakeTeamsService.Object,_fakePublicApplicationFormService.Object,_fakeEmailSenderService.Object,_fakeJobOpeningRepository.Object,_fakeUserRepository.Object,_fakeCurrentHiresRepository.Object);
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

        [Fact]
        public void UpdateStatus_ValidApplicantIdAndStatus_ReturnsViewWithUpdatedModel()
        {
            // Arrange
            var applicantId = It.IsAny<int>();
            var status = "Hired";

            var applicantData = new List<ApplicantListViewModel>
            {
                new ApplicantListViewModel { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 2 },
                new ApplicantListViewModel { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 1 },
                new ApplicantListViewModel { Id = 3, Firstname = "Bob", Lastname = "Johnson", JobApplied = 3 }
            };

            _fakeApplicantListService.Setup(s => s.RetrieveAll()).Returns(applicantData);

            // Act
            var result = _controller.UpdateStatus(applicantId, status);

            // Assert
            Assert.NotNull(result);
            foreach(var applicant in applicantData)
            {
                if(applicant.Id == applicantId)
                {
                    Assert.Equal(status, applicant.Tracker);
                }
            }
            _fakeApplicantListService.Verify(s => s.RetrieveAll(), Times.Once);
        }


        [Fact]
        public void UpdateStatus_InvalidApplicantId_ReturnsNotFound()
        {
            // Arrange
            var applicantId = It.IsAny<int>();
            var status = "Hired";

            var applicantData = new List<ApplicantListViewModel>
            {
                new ApplicantListViewModel { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 2 },
                new ApplicantListViewModel { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 1 },
                new ApplicantListViewModel { Id = 3, Firstname = "Bob", Lastname = "Johnson", JobApplied = 3 }
            };

            _fakeApplicantListService.Setup(s => s.RetrieveAll()).Returns(applicantData);

            // Act
            var result = _controller.UpdateStatus(applicantId, status);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            _fakeApplicantListService.Verify(s => s.RetrieveAll(), Times.Once);
        }
    }
}