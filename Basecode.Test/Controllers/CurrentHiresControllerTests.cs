using Basecode.Data;
using Basecode.Data.Repositories;
using Basecode.WebApp.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Test.Controllers
{
    public class CurrentHiresControllerTests
    {
        private BasecodeContext _context;
        private CurrentHiresRepository _repository;
        private CurrentHiresController _controller;

        public CurrentHiresControllerTests()
        {
            var options = new DbContextOptionsBuilder<BasecodeContext>()
                .UseInMemoryDatabase(databaseName: "HRAutomationSystem")
                .Options;
            _context = new BasecodeContext(options);
            _repository = new CurrentHiresRepository(_context);
            _controller = new CurrentHiresController(_repository);
        }

        [Fact]
        public void CreateInfo_AddsHireToRepository()
        {
            // Arrange
            int applicantID = 1;
            int jobID = 1;

            // Act
            _controller.CreateInfo(applicantID, jobID);

            // Assert
            var addedHire = _context.CurrentHires.SingleOrDefault(h => h.ApplicantID == applicantID && h.JobID == jobID);
            Assert.NotNull(addedHire);
            Assert.Equal(applicantID, addedHire.ApplicantID);
            Assert.Equal(jobID, addedHire.JobID);
        }
    }
}
