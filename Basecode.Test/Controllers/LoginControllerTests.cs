using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class LoginControllerTests
    {
        private readonly LoginController _controller;
        private readonly Mock<ILoginService> _mockLoginService;

        public LoginControllerTests()
        {
            _mockLoginService = new Mock<ILoginService>();
            _controller = new LoginController(_mockLoginService.Object);
        }

        [Fact]
        public void Index_ReturnsView()
        {
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public async void Login_ValidCredentials_RedirectsToHrHomePage()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";

            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };

            var loginData = new SignUpViewModel
            {
                Username = username,
                Password = password
            };

            _mockLoginService.Setup(s => s.GetByUsername(username)).Returns(loginData);

            // Act
            var result = await _controller.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("HrHomePage", result.ControllerName);
        }

        [Fact]
        public async void Login_InvalidCredentials_RedirectsToIndex()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";

            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };

            _mockLoginService.Setup(s => s.GetByUsername(username)).Returns((SignUpViewModel)null);

            // Act
            var result = await _controller.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async void Login_UsernameNotMatched_RedirectsToIndex()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";

            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };

            var loginData = new SignUpViewModel
            {
                Username = "anotheruser",
                Password = password
            };

            _mockLoginService.Setup(s => s.GetByUsername(username)).Returns(loginData);

            // Act
            var result = await _controller.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async void Login_PasswordNotMatched_RedirectsToIndex()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";

            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };

            var loginData = new SignUpViewModel
            {
                Username = username,
                Password = "anotherpassword"
            };

            _mockLoginService.Setup(s => s.GetByUsername(username)).Returns(loginData);

            // Act
            var result = await _controller.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
