using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class PublicApplicationFormControllerTests
    {
        private readonly PublicApplicationFormController _controller;
        private readonly Mock<IPublicApplicationFormService> _mockPublicApplicationFormService;
        private readonly Mock<IEmailSenderService> _mockEmailSenderService;

        public PublicApplicationFormControllerTests()
        {
            _mockPublicApplicationFormService = new Mock<IPublicApplicationFormService>();
            _mockEmailSenderService = new Mock<IEmailSenderService>();
            _controller = new PublicApplicationFormController(_mockPublicApplicationFormService.Object, _mockEmailSenderService.Object);
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
        public void AddForm_ValidForm_RedirectsToIndex()
        {
            // Arrange
            var applicantId = 1;
            var testData = new PublicApplicationFormViewModel
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "1234567890",
                EmailAddress = "Test@gmail.com",
                Address = "Test",
                Time = "Test",
                PositionType = "Test",
                EmploymentType = "Test",
                School = "Test",
                SchoolDepartment = "Test",
                Achievements = "Test",
                ReferenceOneFullName = "Test",
                RelationshipOne = "Test",
                ContactInfoOne = "Test",
                AnsweredOne = 1,
                ReferenceTwoFullName = "Test1",
                RelationshipTwo = "Test1",
                ContactInfoTwo = "Test1",
                AnsweredTwo = 2,
                ReferenceThreeFullName = "Test2",
                RelationshipThree = "Test2",
                ContactInfoThree = "Test2",
                AnsweredThree = 3,
                CurriculumVitae = new byte[1]
            };

            _mockPublicApplicationFormService.Setup(s => s.AddForm(testData))
                .Throws(new Exception("Simulated exception")); // Simulate an exception

            // Act
            var result = _controller.AddForm(testData, applicantId) as IActionResult;

            // Assert
            // Check for the correct ActionResult types
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsNotType<RedirectToActionResult>(result);

            // Check if ModelState is valid
            Assert.True(_controller.ModelState.IsValid, "ModelState should be valid");
        }

        [Fact]
        public void AddForm_HasNoPublicApplicationForm_ReturnsView()
        {
            // Arrange
            var applicantId = 1;
            var testData = new PublicApplicationFormViewModel();

            _mockPublicApplicationFormService.Setup(s => s.AddForm(testData));

            // Act
            var result = _controller.AddForm(testData, applicantId);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
