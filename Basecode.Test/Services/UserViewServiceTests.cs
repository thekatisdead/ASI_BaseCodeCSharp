using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Moq;

namespace Basecode.Test.Services
{
    public class UserViewServiceTests
    {
        private readonly UserViewService _service;
        private readonly Mock<IUserViewRepository> _fakeUserViewRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public UserViewServiceTests()
        {
            _fakeUserViewRepository = new Mock<IUserViewRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new UserViewService(_fakeUserViewRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void RetrieveAll_HasUsers_ReturnsUserViewModelList()
        {
            // Arrange
            var signUpList = new List<SignUp>
            {
                new SignUp
                {
                    Username = "john.doe",
                    Password = "onenation",
                    ConfirmPassword = "onenation",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john.doe@example.com",
                    Role = "Applicant"
                },
                new SignUp
                {
                    Username = "jane.smith",
                    Password = "helloworld",
                    ConfirmPassword = "helloworld",
                    FirstName = "Jane",
                    LastName = "Smith",
                    EmailAddress = "jane.smith@example.com",
                    Role = "HR"
                }
            };

            _fakeUserViewRepository.Setup(repo => repo.RetrieveAll()).Returns(signUpList.AsQueryable());
            _fakeMapper.Setup(mapper => mapper.Map<List<UserViewModel>>(It.IsAny<IEnumerable<SignUp>>())).Returns(new List<UserViewModel>());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserViewModel>>(result);
        }

        [Fact]
        public void RetrieveAll_HasNoUsers_ReturnsEmptyUserViewModelList()
        {
            // Arrange
            var signUpList = new List<SignUp>();

            _fakeUserViewRepository.Setup(repo => repo.RetrieveAll()).Returns(signUpList.AsQueryable());
            _fakeMapper.Setup(mapper => mapper.Map<List<UserViewModel>>(It.IsAny<IEnumerable<SignUp>>())).Returns(new List<UserViewModel>());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserViewModel>>(result);
            Assert.Empty(result);
        }
    }
}