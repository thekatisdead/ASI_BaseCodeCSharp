﻿using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class PublicApplicationFormServiceTests
    {
        private readonly PublicApplicationFormService _service;
        private readonly Mock<IPublicApplicationFormRepository> _fakePublicApplicationFormRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public PublicApplicationFormServiceTests()
        {
            _fakePublicApplicationFormRepository = new Mock<IPublicApplicationFormRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new PublicApplicationFormService(_fakePublicApplicationFormRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void AddForm_GivenValidApplicationForm_CallsRepositoryAddForm()
        {
            // Arrange
            var applicationFormViewModel = new PublicApplicationFormViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                EmailAddress = "john.doe@example.com",
                Address = "Cebu City",
                Time = "9:00 AM",
                PositionType = "Full-time",
                EmploymentType = "Permanent",
                School = "CIT-U",
                SchoolDepartment = "Computer Science",
                Achievements = "Summa Cum Laude",
                ReferenceOneFullName = "Jane Smith",
                RelationshipOne = "Manager",
                ContactInfoOne = "jane.smith@example.com, 123-456-7890",
                ReferenceTwoFullName = "Michael Johnson",
                RelationshipTwo = "Colleague",
                ContactInfoTwo = "michael.johnson@example.com, 987-654-3210",
                ReferenceThreeFullName = "Emily Davis",
                RelationshipThree = "Friend",
                ContactInfoThree = "emily.davis@example.com, 555-123-4567",
                CurriculumVitae = new byte[0]
            };

            _fakeMapper.Setup(mapper => mapper.Map<PublicApplicationForm>(applicationFormViewModel)).Returns(new PublicApplicationForm());

            // Act
            _service.AddForm(applicationFormViewModel);

            // Assert
            _fakePublicApplicationFormRepository.Verify(repo => repo.AddForm(It.IsAny<PublicApplicationForm>()), Times.Once);
        }
    }
}