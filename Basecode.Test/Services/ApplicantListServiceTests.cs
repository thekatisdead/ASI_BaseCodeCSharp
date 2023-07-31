//using Basecode.Data.Interfaces;
//using Basecode.Data.Models;
//using Basecode.Services.Services;
//using Moq;

//namespace Basecode.Tests.Services
//{
//    public class ApplicantListServiceTests
//    {
//        private readonly ApplicantListService _service;
//        private readonly Mock<IApplicantListRepository> _fakeApplicantListRepository;
//        private readonly Mock<IJobOpeningRepository> _fakeJobOpeningRepository;

//        public ApplicantListServiceTests()
//        {
//            _fakeApplicantListRepository = new Mock<IApplicantListRepository>();
//            _fakeJobOpeningRepository= new Mock<IJobOpeningRepository>();
           
//            _service = new ApplicantListService(_fakeApplicantListRepository.Object,_fakeJobOpeningRepository.Object);
//        }

//        [Fact]
//        public void RetrieveAll_HasApplicants_ReturnsAllApplicants()
//        {
//            // Arrange
//            var applicants = new List<Applicant>()
//            {
//                new Applicant { Id = 1, Firstname = "John", Lastname = "Doe", JobApplied = 1 },
//                new Applicant { Id = 2, Firstname = "Jane", Lastname = "Smith", JobApplied = 2 }
//            };

//            _fakeApplicantListRepository.Setup(repo => repo.RetrieveAll()).Returns(applicants.AsQueryable());

//            // Act
//            var result = _service.RetrieveAll();

//            // Assert
//            Assert.Collection(result,
//                item =>
//                {
//                    Assert.Equal(applicants[0].Id, item.Id);
//                    Assert.Equal(applicants[0].Firstname, item.Firstname);
//                    Assert.Equal(applicants[0].Lastname, item.Lastname);
//                    Assert.Equal(applicants[0].JobApplied, item.JobApplied);
//                },
//                item =>
//                {
//                    Assert.Equal(applicants[1].Id, item.Id);
//                    Assert.Equal(applicants[1].Firstname, item.Firstname);
//                    Assert.Equal(applicants[1].Lastname, item.Lastname);
//                    Assert.Equal(applicants[1].JobApplied, item.JobApplied);
//                }
//            );
//        }

//        [Fact]
//        public void RetrieveAll_HasNoApplicants_ReturnsEmptyList()
//        {
//            // Arrange
//            var applicants = new List<Applicant>(); // Empty list

//            _fakeApplicantListRepository.Setup(repo => repo.RetrieveAll()).Returns(applicants.AsQueryable());

//            // Act
//            var result = _service.RetrieveAll();

//            // Assert
//            Assert.Empty(result);
//        }

//        [Fact]
//        public void UpdateStatus_ValidApplicantId_UpdatesApplicantStatus()
//        {
//            // Arrange
//            int applicantId = 1;
//            string newStatus = "Hired";

//            var applicant = new Applicant
//            {
//                Id = applicantId,
//                Firstname = "John",
//                Lastname = "Doe",
//                JobApplied = 1,
//                Tracker = "Pending"
//            };

//            _fakeApplicantListRepository.Setup(repo => repo.GetById(applicantId)).Returns(applicant);

//            // Act
//            _service.UpdateStatus(applicantId, newStatus);

//            // Assert
//            Assert.Equal(newStatus, applicant.Tracker);
//            _fakeApplicantListRepository.Verify(repo => repo.Update(applicant), Times.Once);
//        }
//    }
//}
