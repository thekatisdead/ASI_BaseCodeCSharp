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
        private readonly Mock<IEmailSenderService> _mockEmailSenderService;

        public CharacterReferenceControllerTests()
        {
            _mockCharacterReferenceService = new Mock<ICharacterReferenceService>();
            _mockEmailSenderService = new Mock<IEmailSenderService>();
            _controller = new CharacterReferenceController(_mockCharacterReferenceService.Object, _mockEmailSenderService.Object);
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
        public void ManageRespondents_Success_ReturnsViewWithData()
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
            var result = _controller.ManageRespondents();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(testData, viewResult.Model);
        }

        [Fact]
        public void ManageRespondents_Exception_ReturnsBadRequest()
        {
            // Arrange
            _mockCharacterReferenceService.Setup(s => s.RetrieveAll()).Throws(new Exception("Test exception"));

            // Act
            var result = _controller.ManageRespondents();

            // Assert
            Assert.NotNull(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("An error occurred while generating the character reference respondents list.", badRequestResult.Value);
        }

        [Fact]
        public void GenerateCharacterReferenceReport_ValidRespondentId_ReturnsViewWithData()
        {
            // Arrange
            int respondentId = 1;
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
                },
                // Add more related respondents if needed
            };

            _mockCharacterReferenceService.Setup(s => s.RetrieveResponses()).Returns(testData);

            // Act
            var result = _controller.GenerateCharacterReferenceReport(respondentId);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<List<CharacterReferenceViewModel>>(viewResult.Model);
            var model = (List<CharacterReferenceViewModel>)viewResult.Model;
            Assert.Equal(testData, model);
            Assert.Equal(respondentId, viewResult.ViewData["CurrentRespondentId"]);
        }

        [Fact]
        public void GenerateCharacterReferenceReport_InvalidRespondentId_ReturnsNotFound()
        {
            // Arrange
            int respondentId = 100;
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
                },
                // Add more related respondents if needed
            };

            _mockCharacterReferenceService.Setup(s => s.RetrieveResponses()).Returns(testData);

            // Act
            var result = _controller.GenerateCharacterReferenceReport(respondentId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GenerateCharacterReferenceReport_Exception_ReturnsBadRequest()
        {
            // Arrange
            int respondentId = 1;
            _mockCharacterReferenceService.Setup(s => s.RetrieveResponses()).Throws(new Exception("Test exception"));

            // Act
            var result = _controller.GenerateCharacterReferenceReport(respondentId);

            // Assert
            Assert.NotNull(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("An error occurred while generating the character reference report.", badRequestResult.Value);
        }
    }
}
