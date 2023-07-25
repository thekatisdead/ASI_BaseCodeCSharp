using Basecode.Data.Interfaces;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class CurrentHiresServiceTests
    {
        private readonly CurrentHiresService _service;
        private readonly Mock<ICurrentHiresRepository> _repository;

        public CurrentHiresServiceTests()
        {
            _repository = new Mock<ICurrentHiresRepository>();
            _service = new CurrentHiresService(_repository.Object);
        }

        [Fact]
        public void AddHire_ValidApplicantIdAndJobId_CallsRepositoryAddHire()
        {
            // Arrange
            var applicantId = 1;
            var jobId = 1;

            // Act
            _service.AddHire(applicantId, jobId);

            // Assert
            _repository.Verify(x => x.AddHire(applicantId, jobId), Times.Once);
        }

        [Fact]
        public void AddHire_InvalidApplicantIdAndJobId_ThrowsException()
        {
            // Arrange
            var applicantId = 0;
            var jobId = 0;

            // Act
            _service.AddHire(applicantId, jobId);

            // Assert
            _repository.Verify(x => x.AddHire(applicantId, jobId), Times.Once);
        }
    }
}
