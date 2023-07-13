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

        public PublicApplicationFormControllerTests()
        {
            _mockPublicApplicationFormService = new Mock<IPublicApplicationFormService>();
            _controller = new PublicApplicationFormController(_mockPublicApplicationFormService.Object);
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
            var testData = new PublicApplicationFormViewModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "09123456789",
                EmailAddress = "johndoe@gmail.com",
                Address = "Manila",
                Time = "9:00 AM",
                PositionType = "Full Time",
                EmploymentType = "Contractual",
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

            _mockPublicApplicationFormService.Setup(s => s.AddForm(It.IsAny<PublicApplicationFormViewModel>()));

            // Act
            var result = _controller.AddForm(testData);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("ApplicantHomepage", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void AddForm_HasNoPublicApplicationForm_ReturnsView()
        {
            // Arrange
            var testData = new PublicApplicationFormViewModel();

            _mockPublicApplicationFormService.Setup(s => s.AddForm(testData));

            // Act
            var result = _controller.AddForm(testData);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
