using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Test.Services
{
    public class AdminServiceTests
    {
        private readonly AdminService _service;
        private readonly Mock<IAdminRepository> _fakeAdminRepository;

        public AdminServiceTests()
        {
            _fakeAdminRepository = new Mock<IAdminRepository>();
            _service = new AdminService(_fakeAdminRepository.Object);
        }

        [Fact]
        public async Task CreateRole_ValidRoleName_ReturnsIdentityResultSuccess()
        {
            // Arrange
            var roleName = "AdminRole";

            var identityResult = IdentityResult.Success;

            _fakeAdminRepository.Setup(repo => repo.CreateRole(roleName)).ReturnsAsync(identityResult);

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

            _fakeAdminRepository.Setup(repo => repo.CreateRole(roleName)).ReturnsAsync(identityResult);

            // Act
            var result = await _service.CreateRole(roleName);

            // Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }
    }
}
