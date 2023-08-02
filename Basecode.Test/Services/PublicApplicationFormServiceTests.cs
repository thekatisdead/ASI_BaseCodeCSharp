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
            _fakeJobOpeningRepository= new Mock<IJobOpeningRepository>();
            _service = new PublicApplicationFormService(_fakePublicApplicationFormRepository.Object, _fakeMapper.Object,_fakeApplicantsListRepository.Object,_fakeJobOpeningRepository.Object);
        }

        [Fact]
        public void AddForm_GivenValidApplicationForm_CallsRepositoryAddForm()
        {
            // Arrange
            var applicationFormViewModel = new PublicApplicationFormViewModel
            {

                Id = 1,
                PhoneNumber = "1234567890",
                ApplicantId= 1,
                Position= 1,
                Address = "Test",
                Time = "Test",
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

            _fakeMapper.Setup(mapper => mapper.Map<PublicApplicationForm>(applicationFormViewModel)).Returns(new PublicApplicationForm());

            // Act
            _service.AddFormS(applicationFormViewModel);

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