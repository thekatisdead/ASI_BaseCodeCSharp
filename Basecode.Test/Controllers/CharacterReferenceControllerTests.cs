using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class CharacterReferenceControllerTests
    {
        private readonly CharacterReferenceController _controller;
        private readonly Mock<ICharacterReferenceService> _mockCharacterReferenceService;

        public CharacterReferenceControllerTests()
        {
            _mockCharacterReferenceService = new Mock<ICharacterReferenceService>();
            _controller = new CharacterReferenceController(_mockCharacterReferenceService.Object);
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
        public void Add_HasCharacterReference_ReturnsView()
        {
            // Arrange
            var testData = new CharacterReferenceViewModel
            {
                Id = 1,
                CandidateFirstName = "John",
                CandidateLastName = "Doe",
                Position = "Software Developer",
                RelationshipDuration = "1 year",
                Relationship = "Friend",
                CharacterEthics = "Good",
                Qualifications = "Good",
                FirstName = "Jane",
                LastName = "Doe",
                JobTitle = "Software Developer",
                WorkedWithCandidate = true,
                ReasonToHire = "Good"
            };

            _mockCharacterReferenceService.Setup(s => s.AddCharacterReference(testData));

            // Act
            var result = _controller.Add(testData);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Add_HasNoCharacterReference_ReturnsRedirectToAction()
        {
            // Arrange
            var testData = new CharacterReferenceViewModel();

            _mockCharacterReferenceService.Setup(s => s.AddCharacterReference(testData));

            // Act
            var result = _controller.Add(testData);

            // Assert
            Assert.NotNull(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void GenerateCharacterReferenceReport_Success_ReturnsViewWithData()
        {
            // Arrange
            var testData = new List<CharacterReferenceViewModel>
            {
                new CharacterReferenceViewModel
                {
                    Id = 1,
                    CandidateFirstName = "John",
                    CandidateLastName = "Doe",
                    Position = "Software Developer",
                    RelationshipDuration = "1 year",
                    Relationship = "Friend",
                    CharacterEthics = "Good",
                    Qualifications = "Good",
                    FirstName = "Jane",
                    LastName = "Doe",
                    JobTitle = "Software Developer",
                    WorkedWithCandidate = true,
                    ReasonToHire = "Good"
                }
            };

            _mockCharacterReferenceService.Setup(s => s.RetrieveAll()).Returns(testData);

            // Act
            var result = _controller.GenerateCharacterReferenceReport();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(testData, viewResult.Model);
        }

        [Fact]
        public void GenerateCharacterReferenceReport_Exception_ReturnsBadRequest()
        {
            // Arrange
            _mockCharacterReferenceService.Setup(s => s.RetrieveAll()).Throws(new Exception("Test exception"));

            // Act
            var result = _controller.GenerateCharacterReferenceReport();

            // Assert
            Assert.NotNull(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("An error occurred while generating the report.", badRequestResult.Value);
        }
    }
}
