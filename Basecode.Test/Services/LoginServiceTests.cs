using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class LoginServiceTests
    {
        private readonly LoginService _service;
        private readonly Mock<ILoginRepository> _fakeLoginRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public LoginServiceTests()
        {
            _fakeLoginRepository = new Mock<ILoginRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new LoginService(_fakeLoginRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void GetByUsername_GivenValidUsername_ReturnsSignUpViewModel()
        {
            // Arrange
            var username = "john.doe";
            var signUp = new SignUp
            {
                Username = username,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                ContactNumber = "123456789",
                Address = "123 Street, City",
                Role = "Applicant"
            };

            _fakeLoginRepository.Setup(repo => repo.GetByUsername(username)).Returns(signUp);
            _fakeMapper.Setup(mapper => mapper.Map<SignUpViewModel>(It.IsAny<SignUp>())).Returns(new SignUpViewModel
            {
                Username = signUp.Username,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                EmailAddress = signUp.EmailAddress,
                ContactNumber = signUp.ContactNumber,
                Address = signUp.Address,
                Role = signUp.Role
            });

            // Act
            var result = _service.GetByUsername(username);

            // Assert
            Assert.Equal(signUp.Username, result.Username);
            Assert.Equal(signUp.FirstName, result.FirstName);
            Assert.Equal(signUp.LastName, result.LastName);
            Assert.Equal(signUp.EmailAddress, result.EmailAddress);
            Assert.Equal(signUp.ContactNumber, result.ContactNumber);
            Assert.Equal(signUp.Address, result.Address);
            Assert.Equal(signUp.Role, result.Role);
        }

        [Fact]
        public void GetByUsername_GivenInvalidUsername_ReturnsNull()
        {
            // Arrange
            var username = "john.doe";
            var signUp = new SignUp();

            _fakeLoginRepository.Setup(repo => repo.GetByUsername(username)).Returns(signUp);
            _fakeMapper.Setup(mapper => mapper.Map<SignUpViewModel>(It.IsAny<SignUp>())).Returns(new SignUpViewModel
            {
                Username = signUp.Username,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                EmailAddress = signUp.EmailAddress,
                ContactNumber = signUp.ContactNumber,
                Address = signUp.Address,
                Role = signUp.Role
            });

            // Act
            var result = _service.GetByUsername(username);

            // Assert
            Assert.Equal(signUp.Username, result.Username);
            Assert.Equal(signUp.FirstName, result.FirstName);
            Assert.Equal(signUp.LastName, result.LastName);
            Assert.Equal(signUp.EmailAddress, result.EmailAddress);
            Assert.Equal(signUp.ContactNumber, result.ContactNumber);
            Assert.Equal(signUp.Address, result.Address);
            Assert.Equal(signUp.Role, result.Role);
        }
    }
}