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
            var jobOpenings = new List<JobOpening>
            {
                new JobOpening { Id = 1, Position = "Software Engineer" },
                new JobOpening { Id = 2, Position = "Data Analyst" }
            };

            var applicants = new List<Applicant>
            {
                new Applicant { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 1, Tracker = "Pending" },
                new Applicant { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 2, Tracker = "In Progress" }
            };

            _fakeJobOpeningRepository.Setup(repo => repo.RetrieveAll()).Returns(jobOpenings.AsQueryable());
            _fakeApplicantListRepository.Setup(repo => repo.RetrieveAll()).Returns(applicants.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(applicants.Count, result.Count);

            for (int i = 0; i < applicants.Count; i++)
            {
                Assert.Equal(applicants[i].Id, result[i].Id);
                Assert.Equal(applicants[i].Firstname, result[i].Firstname);
                Assert.Equal(applicants[i].Lastname, result[i].Lastname);
                Assert.Equal(applicants[i].JobApplied, result[i].JobApplied);
                Assert.Equal(jobOpenings.SingleOrDefault(j => j.Id == applicants[i].JobApplied)?.Position, result[i].JobPosition);
                Assert.Equal(applicants[i].Tracker, result[i].Tracker);
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
