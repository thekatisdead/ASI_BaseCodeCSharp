using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class SignUpServiceTests
    {
        private readonly SignUpService _service;
        private readonly Mock<ISignUpRepository> _fakeSignUpRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public SignUpServiceTests()
        {
            _fakeSignUpRepository = new Mock<ISignUpRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new SignUpService(_fakeSignUpRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void CreateAccount_GivenValidAccount_CallsRepositoryCreateAccount()
        {
            // Arrange
            var signUpViewModel = new SignUpViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                ContactNumber = "09887654561",
                Address = "Cebu City",
                Username = "johndoe",
                Password = "123asd",
                ConfirmPassword = "123asd",
                Role = "Applicant"
            };

            _fakeMapper.Setup(mapper => mapper.Map<SignUp>(signUpViewModel)).Returns(new SignUp());

            // Act
            _service.CreateAccount(signUpViewModel);

            // Assert
            _fakeSignUpRepository.Verify(repo => repo.CreateAccount(It.IsAny<SignUp>()), Times.Once);
        }

        [Fact]
        public void CreateAccount_InvalidAccount_ThrowsException()
        {
            // Arrange
            var signUpViewModel = new SignUpViewModel();

            _fakeMapper.Setup(mapper => mapper.Map<SignUp>(signUpViewModel)).Returns(new SignUp());

            // Act
            _service.CreateAccount(signUpViewModel);

            // Assert
            _fakeSignUpRepository.Verify(repo => repo.CreateAccount(It.IsAny<SignUp>()), Times.Once);
        }
    }
}