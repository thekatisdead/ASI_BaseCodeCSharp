using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Basecode.Test.Controllers
{
    public class ApplicationTrackingControllerTests
    {
        private readonly ApplicationTrackingController _controller;
        private readonly ApplicationTrackingRepository _applicationTrackingRepository;
        private readonly JobOpeningRepository _jobOpeningRepository;
        private readonly BasecodeContext _context;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ApplicationTrackingControllerTests()
        {
            var options = new DbContextOptionsBuilder<BasecodeContext>()
                .UseInMemoryDatabase(databaseName: "HRAutomationSystem")
                .Options;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _context = new BasecodeContext(options);
            _applicationTrackingRepository = new ApplicationTrackingRepository(_context);
            _jobOpeningRepository = new JobOpeningRepository(_mockUnitOfWork.Object, _context);
            _controller = new ApplicationTrackingController(_applicationTrackingRepository, _jobOpeningRepository);
        }

      

        // Other test methods
    }
}
