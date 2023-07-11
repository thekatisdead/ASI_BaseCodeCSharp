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
        public void CharacterReference_HasCharacterReference_AddCharacterReference()
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
        public void CharacterReference_HasNoCharacterReference_ReturnsRedirectToAction()
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

    }
}
