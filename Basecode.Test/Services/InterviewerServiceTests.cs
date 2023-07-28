using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class InterviewerServiceTests
    {
        private readonly InterviewerServices _service;
        private readonly Mock<IInterviewerRepository> _fakeInterviewerRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public InterviewerServiceTests()
        {
            _fakeInterviewerRepository = new Mock<IInterviewerRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new InterviewerServices(_fakeInterviewerRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void Add_ValidInterviewer_CallsRepositoryAdd()
        {
            // Arrange
            var interviewer = new Interviewer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                ContactNo = "1234567890"
            };

            _fakeMapper.Setup(mapper => mapper.Map<Interviewer>(It.IsAny<Interviewer>())).Returns(interviewer);

            // Act
            _service.Add(interviewer);

            // Assert
            _fakeInterviewerRepository.Verify(repository => repository.Add(It.IsAny<Interviewer>()), Times.Once);
        }

        [Fact]
        public void GetAll_HasInterviewers_ReturnsListOfInterviewerViewModel()
        {
            // Arrange
            var interviewers = new List<Interviewer>
            {
                new Interviewer
                {
                    InterviewerId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    ContactNo = "1234567890"
                },
                new Interviewer
                {
                    InterviewerId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "janesmith@example.com",
                    ContactNo = "9876543210"
                }
            };

            _fakeInterviewerRepository.Setup(repository => repository.GetAll()).Returns(interviewers.AsQueryable());

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<InterviewerViewModel>>(result);
            Assert.Equal(interviewers.Count, result.Count);
            Assert.Equal(interviewers[0].FirstName, result[0].FirstName);
            Assert.Equal(interviewers[1].LastName, result[1].LastName);
        }

        [Fact]
        public void Update_InterviewerExists_SuccessfullyUpdatesInterviewer()
        {
            // Arrange
            int interviewerId = 1;
            var updatedInterviewer = new Interviewer
            {
                InterviewerId = interviewerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                ContactNo = "9876543210"
            };

            var existingInterviewer = new Interviewer
            {
                InterviewerId = interviewerId,
                FirstName = "James",
                LastName = "Dean",
                Email = "jamesdean@example.com",
                ContactNo = "1234567890"
            };

            _fakeInterviewerRepository.Setup(repository => repository.GetById(interviewerId)).Returns(existingInterviewer);

            // Act
            _service.Update(updatedInterviewer);

            // Assert
            _fakeInterviewerRepository.Verify(repository => repository.Update(It.IsAny<Interviewer>()), Times.Once);
            Assert.Equal(updatedInterviewer.FirstName, existingInterviewer.FirstName);
            Assert.Equal(updatedInterviewer.LastName, existingInterviewer.LastName);
            Assert.Equal(updatedInterviewer.Email, existingInterviewer.Email);
            Assert.Equal(updatedInterviewer.ContactNo, existingInterviewer.ContactNo);
        }

        [Fact]
        public void GetById_InterviewerExists_ReturnsInterviewerViewModel()
        {
            // Arrange
            int interviewerId = 1;
            var existingInterviewer = new Interviewer
            {
                InterviewerId = interviewerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                ContactNo = "9876543210"
            };

            var expectedViewModel = new InterviewerViewModel
            {
                InterviewerId = existingInterviewer.InterviewerId,
                FirstName = existingInterviewer.FirstName,
                LastName = existingInterviewer.LastName,
                Email = existingInterviewer.Email,
                ContactNo = existingInterviewer.ContactNo
            };

            _fakeInterviewerRepository.Setup(repository => repository.GetById(interviewerId)).Returns(existingInterviewer);
            _fakeMapper.Setup(mapper => mapper.Map<InterviewerViewModel>(existingInterviewer)).Returns(expectedViewModel);

            // Act
            var result = _service.GetById(interviewerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedViewModel.InterviewerId, result.InterviewerId);
            Assert.Equal(expectedViewModel.FirstName, result.FirstName);
            Assert.Equal(expectedViewModel.LastName, result.LastName);
            Assert.Equal(expectedViewModel.Email, result.Email);
            Assert.Equal(expectedViewModel.ContactNo, result.ContactNo);
        }

        [Fact]
        public void GetById_InterviewerDoesNotExist_ReturnsNull()
        {
            // Arrange
            int interviewerId = 1;
            Interviewer nullInterviewer = null;

            _fakeInterviewerRepository.Setup(repository => repository.GetById(interviewerId)).Returns(nullInterviewer);

            // Act
            var result = _service.GetById(interviewerId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_InterviewerExists_SuccessfullyDeletesInterviewer()
        {
            // Arrange
            int interviewerId = 1;
            var existingInterviewer = new Interviewer
            {
                InterviewerId = interviewerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                ContactNo = "9876543210"
            };

            _fakeInterviewerRepository.Setup(repository => repository.GetById(interviewerId)).Returns(existingInterviewer);

            // Act
            _service.Delete(interviewerId);

            // Assert
            _fakeInterviewerRepository.Verify(repository => repository.Delete(existingInterviewer), Times.Once);
        }
    }
}
