using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.Test.Controllers
{
    public class DashboardControllerTests
    {
        private readonly DashboardController _controller;

        public DashboardControllerTests()
        {
            _controller = new DashboardController();
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
    }
}
