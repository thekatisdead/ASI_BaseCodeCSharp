using AutoMapper;
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
        private readonly Mock<IApplicantListRepository> _fakeApplicantsListRepository;
        private readonly Mock<IJobOpeningRepository> _fakeJobOpeningRepository;

        public PublicApplicationFormServiceTests()
        {
            _fakePublicApplicationFormRepository = new Mock<IPublicApplicationFormRepository>();
            _fakeMapper = new Mock<IMapper>();
            _fakeApplicantsListRepository= new Mock<IApplicantListRepository>();        
            _service = new PublicApplicationFormService(_fakePublicApplicationFormRepository.Object, _fakeMapper.Object,_fakeApplicantsListRepository.Object,_fakeJobOpeningRepository.Object);
        }

        [Fact]
        public void AddForm_GivenValidApplicationForm_CallsRepositoryAddForm()
        {
            // Arrange
            var applicationFormViewModel = new PublicApplicationFormViewModel
            {
                
                PhoneNumber = "123456789",
               
                Address = "Cebu City",
                Time = "9:00 AM",
               
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

        [Fact]
        public void GetById_ExistingFormId_ReturnsPublicApplicationFormViewModel()
        {
            // Arrange
            int formId = 1;

            var applicationForm = new PublicApplicationForm();
            var formViewModel = new PublicApplicationFormViewModel();

            _fakePublicApplicationFormRepository.Setup(repo => repo.GetById(formId)).Returns(applicationForm);
            _fakeMapper.Setup(mapper => mapper.Map<PublicApplicationFormViewModel>(applicationForm)).Returns(formViewModel);

            // Act
            var result = _service.GetById(formId);

            // Assert
            Assert.Equal(formViewModel, result);
        }
    }
}