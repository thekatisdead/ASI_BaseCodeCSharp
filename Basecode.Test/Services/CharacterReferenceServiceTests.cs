using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class CharacterReferenceServiceTests
    {
        private readonly CharacterReferenceService _service;
        private readonly Mock<ICharacterReferenceRepository> _fakeCharacterReferenceRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public CharacterReferenceServiceTests()
        {
            _fakeCharacterReferenceRepository = new Mock<ICharacterReferenceRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new CharacterReferenceService(_fakeCharacterReferenceRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void AddCharacterReference_GivenValidCharacterReferenceViewModel_AddsCharacterReferenceToRepository()
        {
            // Arrange
            var characterReferenceViewModel = new CharacterReferenceViewModel
            {
                CandidateFirstName = "John",
                CandidateLastName = "Doe",
                Position = "Manager",
                RelationshipDuration = "5 years",
                Relationship = "Friend",
                CharacterEthics = "Excellent",
                Qualifications = "Good communication skills",
                FirstName = "Jane",
                LastName = "Smith",
                JobTitle = "Supervisor",
                WorkedWithCandidate = true,
                ReasonToHire = "Strong leadership skills",
                CreatedTime = DateTime.Now,
                CreatedBy = Environment.UserName
            };

            var characterReference = new CharacterReference
            {
                CandidateFirstName = "John",
                CandidateLastName = "Doe",
                Position = "Manager",
                RelationshipDuration = "5 years",
                Relationship = "Friend",
                CharacterEthics = "Excellent",
                Qualifications = "Good communication skills",
                FirstName = "Jane",
                LastName = "Smith",
                JobTitle = "Supervisor",
                WorkedWithCandidate = true,
                ReasonToHire = "Strong leadership skills",
                CreatedTime = DateTime.Now,
                CreatedBy = Environment.UserName
            };

            _fakeMapper.Setup(mapper => mapper.Map<CharacterReference>(characterReferenceViewModel))
                .Returns(characterReference);

            // Act
            _service.AddCharacterReference(characterReferenceViewModel);

            // Assert
            _fakeCharacterReferenceRepository.Verify(repo => repo.Add(characterReference), Times.Once);
        }

        [Fact]
        public void AddCharacterReference_GivenInvalidCharacterReferenceViewModel_ThrowsException()
        {
            // Arrange
            var characterReferenceViewModel = new CharacterReferenceViewModel();
            var characterReference = new CharacterReference();

            _fakeMapper.Setup(mapper => mapper.Map<CharacterReference>(characterReferenceViewModel))
                .Returns(characterReference) ;

            // Act
            _service.AddCharacterReference(characterReferenceViewModel);

            // Assert
            _fakeCharacterReferenceRepository.Verify(repo => repo.Add(characterReference), Times.Once);
        }
    }
}