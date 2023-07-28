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
        private readonly Mock<IJobOpeningService> _mockJobOpeningService;
        private readonly Mock<IApplicantListService> _mockApplicantListService;
        public PublicApplicationFormControllerTests()
        {
            _mockPublicApplicationFormService = new Mock<IPublicApplicationFormService>();
            _mockEmailSenderService = new Mock<IEmailSenderService>();
            _mockApplicantListService= new Mock<IApplicantListService>();
            _mockJobOpeningService= new Mock<IJobOpeningService>();
            _controller = new PublicApplicationFormController(_mockPublicApplicationFormService.Object, _mockEmailSenderService.Object,_mockApplicantListService.Object,_mockJobOpeningService.Object);
        }

        [Fact]
        public void Index_ReturnsView()
        {
            // Act
            var result = _controller.Index(1) as ViewResult;

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
                PhoneNumber = "09123456789",               
                Address = "Manila",
                Time = "9:00 AM",
                School = "University of the Philippines",
                SchoolDepartment = "Computer Science",
                Achievements = "Dean's Lister",
                ReferenceOneFullName = "Jane Doe",
                RelationshipOne = "Friend",
                ContactInfoOne = "09123456789",
                ReferenceTwoFullName = "Janee Doe",
                RelationshipTwo = "Friend",
                ContactInfoTwo = "09123456789",
                ReferenceThreeFullName = "Janne Doe",
                RelationshipThree = "Friend",
                ContactInfoThree = "09123456789",
                CurriculumVitae = new byte[0],
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
