using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class JobOpeningServiceTests
    {
        private readonly JobOpeningService _service;
        private readonly Mock<IJobOpeningRepository> _fakeJobOpeningRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public JobOpeningServiceTests()
        {
            _fakeJobOpeningRepository = new Mock<IJobOpeningRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new JobOpeningService(_fakeJobOpeningRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void RetrieveAll_HasJobOpenings_ReturnsAllJobOpenings()
        {
            // Arrange
            var jobOpenings = new List<JobOpening>()
            {
                new JobOpening { Id = 1, Position = "Software Engineer", JobType = "Full-time", Salary = 5000, Hours = 8, Shift = "Morning", Description = "Developing Websites" },
                new JobOpening { Id = 2, Position = "Data Analyst", JobType = "Part-time", Salary = 3000, Hours = 6, Shift = "Morning", Description = "Analyzes Data" }
            };

            _fakeJobOpeningRepository.Setup(repo => repo.RetrieveAll()).Returns(jobOpenings.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(jobOpenings[0].Id, item.Id);
                    Assert.Equal(jobOpenings[0].Position, item.Position);
                    Assert.Equal(jobOpenings[0].JobType, item.JobType);
                    Assert.Equal(jobOpenings[0].Salary, item.Salary);
                    Assert.Equal(jobOpenings[0].Hours, item.Hours);
                    Assert.Equal(jobOpenings[0].Shift, item.Shift);
                    Assert.Equal(jobOpenings[0].Description, item.Description);
                },
                item =>
                {
                    Assert.Equal(jobOpenings[1].Id, item.Id);
                    Assert.Equal(jobOpenings[1].Position, item.Position);
                    Assert.Equal(jobOpenings[1].JobType, item.JobType);
                    Assert.Equal(jobOpenings[1].Salary, item.Salary);
                    Assert.Equal(jobOpenings[1].Hours, item.Hours);
                    Assert.Equal(jobOpenings[1].Shift, item.Shift);
                    Assert.Equal(jobOpenings[1].Description, item.Description);
                }
            );
        }

        [Fact]
        public void GetById_GivenValidId_ReturnsJobOpening()
        {
            // Arrange
            var jobOpeningId = 1;
            var jobOpening = new JobOpening { Id = jobOpeningId, Position = "Software Engineer", JobType = "Full-time", Salary = 5000, Hours = 8, Shift = "Morning", Description = "Job description" };

            _fakeJobOpeningRepository.Setup(repo => repo.GetById(jobOpeningId)).Returns(jobOpening);
            _fakeMapper.Setup(mapper => mapper.Map<JobOpeningViewModel>(jobOpening)).Returns(new JobOpeningViewModel
            {
                Id = jobOpening.Id,
                Position = jobOpening.Position,
                JobType = jobOpening.JobType,
                Salary = jobOpening.Salary,
                Hours = jobOpening.Hours,
                Shift = jobOpening.Shift,
                Description = jobOpening.Description
            });

            // Act
            var result = _service.GetById(jobOpeningId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(jobOpening.Id, result.Id);
            Assert.Equal(jobOpening.Position, result.Position);
            Assert.Equal(jobOpening.JobType, result.JobType);
            Assert.Equal(jobOpening.Salary, result.Salary);
            Assert.Equal(jobOpening.Hours, result.Hours);
            Assert.Equal(jobOpening.Shift, result.Shift);
            Assert.Equal(jobOpening.Description, result.Description);
        }

        [Fact]
        public void Add_GivenValidJobOpening_AddsJobOpeningToRepository()
        {
            // Arrange
            var jobOpening = new JobOpening
            {
                Position = "Software Engineer",
                JobType = "Full-time",
                Salary = 5000,
                Hours = 8,
                Shift = "Morning",
                Description = "Job description"
            };

            // Act
            _service.Add(jobOpening);

            // Assert
            _fakeJobOpeningRepository.Verify(repo => repo.Add(jobOpening), Times.Once);
        }

        [Fact]
        public void Update_GivenValidJobOpening_UpdatesJobOpeningInRepository()
        {
            // Arrange
            var jobOpeningId = 1;
            var updatedJobOpening = new JobOpening
            {
                Id = jobOpeningId,
                Position = "Software Engineer",
                JobType = "Full-time",
                Salary = 6000,
                Hours = 8,
                Shift = "Morning",
                Description = "Updated job description"
            };
            var existingJobOpening = new JobOpening
            {
                Id = jobOpeningId,
                Position = "Software Engineer",
                JobType = "Full-time",
                Salary = 5000,
                Hours = 8,
                Shift = "Morning",
                Description = "Job description"
            };

            _fakeJobOpeningRepository.Setup(repo => repo.GetById(jobOpeningId)).Returns(existingJobOpening);

            // Act
            _service.Update(updatedJobOpening);

            // Assert
            Assert.Equal(updatedJobOpening.Salary, existingJobOpening.Salary);
            Assert.Equal(updatedJobOpening.Description, existingJobOpening.Description);
            _fakeJobOpeningRepository.Verify(repo => repo.Update(existingJobOpening), Times.Once);
        }

        [Fact]
        public void Delete_GivenValidId_DeletesJobOpeningFromRepository()
        {
            // Arrange
            var jobOpeningId = 1;
            var jobOpening = new JobOpening { Id = jobOpeningId, Position = "Software Engineer", JobType = "Full-time", Salary = 5000, Hours = 8, Shift = "Morning", Description = "Job description" };

            _fakeJobOpeningRepository.Setup(repo => repo.GetById(jobOpeningId)).Returns(jobOpening);

            // Act
            _service.Delete(jobOpeningId);

            // Assert
            _fakeJobOpeningRepository.Verify(repo => repo.Delete(jobOpening), Times.Once);
        }
    }
}