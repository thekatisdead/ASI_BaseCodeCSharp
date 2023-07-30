using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace Basecode.Tests.Services
{
    public class ApplicantListServiceTests
    {
        private readonly ApplicantListService _service;
        private readonly Mock<IApplicantListRepository> _fakeApplicantListRepository;
        private readonly Mock<IJobOpeningRepository> _fakeJobOpeningRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public ApplicantListServiceTests()
        {
            _fakeApplicantListRepository = new Mock<IApplicantListRepository>();
            _fakeJobOpeningRepository= new Mock<IJobOpeningRepository>();
            _fakeMapper = new Mock<IMapper>();

            _service = new ApplicantListService(_fakeApplicantListRepository.Object,_fakeJobOpeningRepository.Object);
        }

        [Fact]
        public void RetrieveAll_HasApplicants_ReturnsAllApplicants()
        {
            // Arrange
            var applicants = new List<Applicant>()
            {
                new Applicant { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 1 },
                new Applicant { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 2 }
            };

            var applicantList = new List<ApplicantListViewModel>()
            {
                new ApplicantListViewModel { Id = 1, Firstname = "John", Lastname = "Doe", EmailAddress = "sminth@gmail.com", Tracker = "Pending", Grading = "a", JobApplied = 2, JobPosition = "jksd" },
                new ApplicantListViewModel { Id = 2, Firstname = "Jane", Lastname = "Smith", EmailAddress = "sminthsss@gmail.com", Tracker = "Pending", Grading = "b", JobApplied = 2, JobPosition = "jksd" },
            };

            _fakeApplicantListRepository.Setup(repo => repo.RetrieveAll()).Returns(applicants.AsQueryable());
            _fakeMapper.Setup(mapper => mapper.Map<List<ApplicantListViewModel>>(applicants)).Returns(applicantList);

            // Act
            var result = _service.RetrieveAll(); // Use the actual service to invoke the method

            // Assert
            Assert.Equal(applicantList.Count, result.Count);

            for (int i = 0; i < applicantList.Count; i++)
            {
                Assert.Equal(applicantList[i].Id, result[i].Id);
                Assert.Equal(applicantList[i].Firstname, result[i].Firstname);
                Assert.Equal(applicantList[i].Lastname, result[i].Lastname);
                Assert.Equal(applicantList[i].JobApplied, result[i].JobApplied);
                // Add more Assert statements if needed for other properties
            }
        }
        
        [Fact]
        public void RetrieveAll_HasNoApplicants_ReturnsEmptyList()
        {
            // Arrange
            var applicants = new List<Applicant>(); // Empty list

            _fakeApplicantListRepository.Setup(repo => repo.RetrieveAll()).Returns(applicants.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void UpdateStatus_ValidApplicantId_UpdatesApplicantStatus()
        {
            // Arrange
            int applicantId = 1;
            string newStatus = "Hired";

            var applicant = new Applicant
            {
                Id = applicantId,
                Firstname = "John",
                Lastname = "Doe",
                JobApplied = 1,
                Tracker = "Pending"
            };

            _fakeApplicantListRepository.Setup(repo => repo.GetById(applicantId)).Returns(applicant);

            // Act
            _service.UpdateStatus(applicantId, newStatus);

            // Assert
            Assert.Equal(newStatus, applicant.Tracker);
            _fakeApplicantListRepository.Verify(repo => repo.Update(applicant), Times.Once);
        }
    }
}
