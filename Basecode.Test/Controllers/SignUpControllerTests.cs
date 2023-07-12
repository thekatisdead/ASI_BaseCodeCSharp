using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Basecode.Test.Controllers
{
    public class SignUpControllerTests
    {
        private readonly SignUpController _controller;
        private readonly Mock<ISignUpService> _mockSignUpService;

        public SignUpControllerTests()
        {
            _mockSignUpService = new Mock<ISignUpService>();
            _controller = new SignUpController(_mockSignUpService.Object);
        }

        [Fact]
        public void CreateAccount_ValidAccount_RedirectsToIndex()
        {
            // Arrange
            var newAccount = new SignUpViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@example.com",
                ContactNumber = "1234567890",
                Address = "123 Main St",
                Username = "johndoe",
                Password = "king",
                ConfirmPassword = "king",
                Role = "Applicant"
            };

            _mockSignUpService.Setup(service => service.CreateAccount(It.IsAny<SignUpViewModel>()));

            var controller = new SignUpController(_mockSignUpService.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = controller.CreateAccount(newAccount);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            // Assert TempData
            Assert.Equal("Successfully created an account! You may proceed to log in to our system.", controller.TempData["SuccessMessage"]);
        }
    }
}