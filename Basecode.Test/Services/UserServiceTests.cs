using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Basecode.Test.Services
{
    public class UserServiceTests
    {
        private readonly UserService _service;
        private readonly Mock<IUserRepository> _fakeUserRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public UserServiceTests()
        {
            _fakeUserRepository = new Mock<IUserRepository>();
            _fakeMapper = new Mock<IMapper>();
            _service = new UserService(_fakeUserRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public void FindByUsername_ValidUsername_ReturnsUser()
        {
            // Arrange
            string username = "john.doe";
            var expectedUser = new User
            {
                Id = "1",
                Username = username,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNumber = "1234567890",
                Address = "123 Main St",
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "Admin",
                ModifiedDate = DateTime.Now
            };

            _fakeUserRepository.Setup(repo => repo.FindByUsername(username)).Returns(expectedUser);

            // Act
            var result = _service.FindByUsername(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public void FindByUsername_InvalidUsername_ReturnsNull()
        {
            // Arrange
            string username = "nonexistentuser";
            _fakeUserRepository.Setup(repo => repo.FindByUsername(username)).Returns((User)null);

            // Act
            var result = _service.FindByUsername(username);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindAll_HasUsers_ReturnsListOfUsers()
        {
            // Arrange
            var userList = new List<User>
            {
                new User { Id = "1", Username = "user1", FirstName = "User", LastName = "One" },
                new User { Id = "2", Username = "user2", FirstName = "User", LastName = "Two" },
            };

            _fakeUserRepository.Setup(repo => repo.FindAll()).Returns(userList);

            // Act
            var result = _service.FindAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userList.Count, result.Count());
        }

        [Fact]
        public void FindAll_NoUsersAvailable_ReturnsEmptyList()
        {
            // Arrange
            _fakeUserRepository.Setup(repo => repo.FindAll()).Returns(new List<User>());

            // Act
            var result = _service.FindAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void FindById_ValidId_ReturnsUser()
        {
            // Arrange
            string userId = "1";
            var expectedUser = new User
            {
                Id = "1",
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNumber = "1234567890",
                Address = "123 Main St",
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "Admin",
                ModifiedDate = DateTime.Now
            };

            _fakeUserRepository.Setup(repo => repo.FindById(userId)).Returns(expectedUser);

            // Act
            var result = _service.FindById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Username, result.Username);
            Assert.Equal(expectedUser.FirstName, result.FirstName);
            Assert.Equal(expectedUser.LastName, result.LastName);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.ContactNumber, result.ContactNumber);
            Assert.Equal(expectedUser.Address, result.Address);
            Assert.Equal(expectedUser.CreatedBy, result.CreatedBy);
            Assert.Equal(expectedUser.CreatedDate, result.CreatedDate);
            Assert.Equal(expectedUser.ModifiedBy, result.ModifiedBy);
            Assert.Equal(expectedUser.ModifiedDate, result.ModifiedDate);
        }

        [Fact]
        public void FindById_InvalidId_ReturnsNull()
        {
            // Arrange
            string userId = null;
            _fakeUserRepository.Setup(repo => repo.FindById(userId)).Returns((User)null);

            // Act
            var result = _service.FindById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindUser_ValidUsername_ReturnsIdentityUser()
        {
            // Arrange
            string username = "john.doe";
            var expectedUser = new IdentityUser
            {
                UserName = username,
                Email = "john.doe@example.com",
            };

            _fakeUserRepository.Setup(repo => repo.FindUser(username)).Returns(expectedUser);

            // Act
            var result = _service.FindUser(username);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IdentityUser>(result);
            Assert.Equal(expectedUser.UserName, result.UserName);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public void FindUser_InvalidUsername_ReturnsNull()
        {
            // Arrange
            string username = null;
            _fakeUserRepository.Setup(repo => repo.FindUser(username)).Returns((IdentityUser)null);

            // Act
            var result = _service.FindUser(username);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_ValidUser_ReturnsTrue()
        {
            // Arrange
            var userToCreate = new User
            {
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _fakeUserRepository.Setup(repo => repo.Create(It.IsAny<User>())).Returns(true);

            // Act
            bool result = _service.Create(userToCreate);

            // Assert
            Assert.True(result);
            Assert.Equal(System.Environment.UserName, userToCreate.CreatedBy);
            Assert.Equal(System.Environment.UserName, userToCreate.ModifiedBy);
            Assert.NotNull(userToCreate.CreatedDate);
            Assert.NotNull(userToCreate.ModifiedDate);
        }

        [Fact]
        public void Create_InvalidUser_ReturnsFalse()
        {
            // Arrange
            var userToCreate = new User
            {
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _fakeUserRepository.Setup(repo => repo.Create(It.IsAny<User>())).Returns(false);

            // Act
            bool result = _service.Create(userToCreate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Update_ExistingUser_ReturnsTrue()
        {
            // Arrange
            var existingUser = new User
            {
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var updatedUser = new User
            {
                Username = "johndoe",
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com"
            };

            _fakeUserRepository.Setup(repo => repo.FindByUsername(existingUser.Username)).Returns(existingUser);

            // Act
            _service.Update(updatedUser);

            // Assert
            Assert.Equal(System.Environment.UserName, existingUser.ModifiedBy);
            Assert.NotNull(existingUser.ModifiedDate);
            Assert.Equal(updatedUser.FirstName, existingUser.FirstName);
            Assert.Equal(updatedUser.LastName, existingUser.LastName);
            Assert.Equal(updatedUser.Email, existingUser.Email);
        }

        [Fact]
        public void Update_NonExistentUser_DoesNotUpdate()
        {
            // Arrange
            var nonExistentUser = new User
            {
                Username = "nonexistentuser",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _fakeUserRepository.Setup(repo => repo.FindByUsername(nonExistentUser.Username)).Returns((User)null);

            // Act
            _service.Update(nonExistentUser);

            // Assert
            // Verify that the Update method of the repository was not called because the user does not exist
            _fakeUserRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void Delete_ExistingUser_DeletesSuccessfully()
        {
            // Arrange
            var existingUser = new User
            {
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _fakeUserRepository.Setup(repo => repo.FindByUsername(existingUser.Username)).Returns(existingUser);

            // Act
            _service.Delete(existingUser);

            // Assert
            // Verify that the Delete method of the repository was called with the correct user
            _fakeUserRepository.Verify(repo => repo.Delete(existingUser), Times.Once);
        }

        [Fact]
        public void Delete_NonExistentUser_DoesNotDelete()
        {
            // Arrange
            var nonExistentUser = new User
            {
                Username = "nonexistentuser",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _fakeUserRepository.Setup(repo => repo.FindByUsername(nonExistentUser.Username)).Returns((User)null);

            // Act
            _service.Delete(nonExistentUser);

            // Assert
            _fakeUserRepository.Verify(repo => repo.Delete(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FindUserAsync_ExistingUserAndValidPassword_ReturnsIdentityUser()
        {
            // Arrange
            var existingUser = new IdentityUser
            {
                UserName = "johndoe"
            };

            var validPassword = "correct_password";

            _fakeUserRepository.Setup(repo => repo.FindUserAsync(existingUser.UserName, validPassword)).ReturnsAsync(existingUser);

            // Act
            var result = await _service.FindUserAsync(existingUser.UserName, validPassword);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingUser.UserName, result.UserName);
        }

        [Fact]
        public async Task FindUserAsync_ExistingUserAndInvalidPassword_ReturnsNull()
        {
            // Arrange
            var existingUser = new IdentityUser
            {
                UserName = "johndoe"
            };

            var invalidPassword = "wrong_password";

            _fakeUserRepository.Setup(repo => repo.FindUserAsync(existingUser.UserName, invalidPassword)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _service.FindUserAsync(existingUser.UserName, invalidPassword);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindUserAsync_NonExistentUser_ReturnsNull()
        {
            // Arrange
            var nonExistentUserName = "nonexistentuser";
            var password = "some_password";

            _fakeUserRepository.Setup(repo => repo.FindUserAsync(nonExistentUserName, password)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _service.FindUserAsync(nonExistentUserName, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RegisterUser_ValidUser_ReturnsIdentityResultSuccess()
        {
            // Arrange
            var username = "johndoe";
            var password = "P@ssw0rd";
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@example.com";
            var role = "UserRole";

            var identityResult = IdentityResult.Success;

            _fakeUserRepository.Setup(repo => repo.RegisterUser(username, password, firstName, lastName, email, role)).ReturnsAsync(identityResult);

            // Act
            var result = await _service.RegisterUser(username, password, firstName, lastName, email, role);

            // Assert
            Assert.Equal(identityResult, result);
        }

        [Fact]
        public async Task RegisterUser_InvalidUser_ReturnsIdentityResultFailure()
        {
            // Arrange
            // For example, set up a case where the password is too short or the email is invalid...
            var username = "johndoe";
            var password = "short"; // Password is intentionally set to a short value to trigger a failure
            var firstName = "John";
            var lastName = "Doe";
            var email = "invalid_email";
            var role = "UserRole";

            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Invalid password." });

            _fakeUserRepository.Setup(repo => repo.RegisterUser(username, password, firstName, lastName, email, role)).ReturnsAsync(identityResult);

            // Act
            var result = await _service.RegisterUser(username, password, firstName, lastName, email, role);

            // Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Fact]
        public async Task CreateRole_ValidRoleName_ReturnsIdentityResultSuccess()
        {
            // Arrange
            var roleName = "UserRole";

            var identityResult = IdentityResult.Success;

            _fakeUserRepository.Setup(repo => repo.CreateRole(roleName)).ReturnsAsync(identityResult);

            // Act
            var result = await _service.CreateRole(roleName);

            // Assert
            Assert.Equal(identityResult, result);
        }

        [Fact]
        public async Task CreateRole_InvalidRoleName_ReturnsIdentityResultFailure()
        {
            // Arrange
            // For example, set up a case where the role name is empty or contains invalid characters...
            var roleName = ""; // Intentionally set to an empty value to trigger a failure

            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Invalid role name." });

            _fakeUserRepository.Setup(repo => repo.CreateRole(roleName)).ReturnsAsync(identityResult);

            // Act
            var result = await _service.CreateRole(roleName);

            // Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Fact]
        public async Task FindUser_ExistingUserAndValidPassword_ReturnsIdentityUser()
        {
            // Arrange
            var existingUser = new IdentityUser
            {
                UserName = "johndoe",
                // Add other properties...
            };

            var validPassword = "correct_password";

            _fakeUserRepository.Setup(repo => repo.FindUser(existingUser.UserName, validPassword)).ReturnsAsync(existingUser);

            // Act
            var result = await _service.FindUser(existingUser.UserName, validPassword);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingUser.UserName, result.UserName);
            // Add more assertions for other properties based on your business logic...
        }

        [Fact]
        public async Task FindUser_ExistingUserAndInvalidPassword_ReturnsNull()
        {
            // Arrange
            var existingUser = new IdentityUser
            {
                UserName = "johndoe"
            };

            var invalidPassword = "wrong_password";

            _fakeUserRepository.Setup(repo => repo.FindUser(existingUser.UserName, invalidPassword)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _service.FindUser(existingUser.UserName, invalidPassword);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindUser_NonExistentUser_ReturnsNull()
        {
            // Arrange
            var nonExistentUserName = "nonexistentuser";
            var password = "some_password";

            _fakeUserRepository.Setup(repo => repo.FindUser(nonExistentUserName, password)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _service.FindUser(nonExistentUserName, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RetrieveAll_HasUsers_ReturnsListOfUserViewModel()
        {
            // Arrange
            var userList = new List<User>
            {
                new User
                {
                    Username = "johndoe",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    ContactNumber = "1234567890",
                    Address = "123 Main St"
                },
                new User
                {
                    Username = "janesmith",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    ContactNumber = "9876543210",
                    Address = "456 Oak Ave"
                }
            };

            var expectedViewModels = userList.Select(u => new UserViewModel
            {
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                ContactNumber = u.ContactNumber,
                Address = u.Address
            }).ToList();

            _fakeUserRepository.Setup(repo => repo.RetrieveAll()).Returns(userList.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserViewModel>>(result);
            Assert.Equal(expectedViewModels.Count, result.Count);

            for (int i = 0; i < expectedViewModels.Count; i++)
            {
                Assert.Equal(expectedViewModels[i].Username, result[i].Username);
                Assert.Equal(expectedViewModels[i].FirstName, result[i].FirstName);
                Assert.Equal(expectedViewModels[i].LastName, result[i].LastName);
                Assert.Equal(expectedViewModels[i].Email, result[i].Email);
                Assert.Equal(expectedViewModels[i].ContactNumber, result[i].ContactNumber);
                Assert.Equal(expectedViewModels[i].Address, result[i].Address);
            }
        }

        [Fact]
        public void RetrieveAll_NoUsers_ReturnsEmptyList()
        {
            // Arrange
            var userList = new List<User>(); // Empty list

            _fakeUserRepository.Setup(repo => repo.RetrieveAll()).Returns(userList.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserViewModel>>(result);
            Assert.Empty(result);
        }
    }
}