using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.Test.Controllers
{
    public class HrHomepageControllerTests
    {
        private readonly HrHomepageController _controller;

        public HrHomepageControllerTests()
        {
            _controller = new HrHomepageController();
        }

        [Fact]
        public void Index_ReturnsView()
        {
            // Act
            var result = _controller.Index("TestUsername");

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }
    }
}