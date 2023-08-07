using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class CurrentHiresControllerTests
    {
        private readonly CurrentHiresController _controller;
        private readonly Mock<ICurrentHiresService> _mockLoginService;

        public CurrentHiresControllerTests()
        {
            _mockLoginService = new Mock<ICurrentHiresService>();
            _controller = new CurrentHiresController(_mockLoginService.Object);
        }

        [Fact]
        public void Index_ReturnsView()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public void CreateInfo_AddsHireToRepository()
        {
            // Arrange
            int applicantID = 1;
            int jobID = 1;

            // Act
            _mockLoginService.Setup(s => s.AddHire(applicantID, jobID));

            // Assert
            var addedHire = _controller.CreateInfo(applicantID, jobID);

            Assert.NotNull(addedHire);
            var viewResult = Assert.IsType<ViewResult>(addedHire);
            Assert.Null(viewResult.ViewName);
        }
    }
}
