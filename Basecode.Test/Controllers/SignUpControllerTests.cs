using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Test.Controllers
{
    public class SignUpControllerTests
    {
        private readonly SignUpController _controller;
        private readonly Mock<ISignUpService> _mockSignUpService;

        public SignUpControllerTests()
        {
            _mockSignUpService = new Mock<ISignUpService>();
            _controller = new SignUpController(_mockSignUpService.Object);
        }

        //[Fact]
        //public void SignUp_IsValid_ReturnsRedirectToAction()
        //{
        //    Arrange
        //    var testData = new SignUpViewModel
        //    {
        //        Id = 1,
        //        FirstName = "John",
        //        LastName = "Doe",
        //        EmailAddress = "johndoe@gmail.com",
        //        ContactNumber = "09123456789",
        //        Address = "Manila",
        //        Username = "johndoe",
        //        Password = "123456",
        //        ConfirmPassword = "123456",
        //        Role = "Applicant"
        //    };

        //    _mockSignUpService.Setup(s => s.CreateAccount(testData));

        //     Act
        //    var result = _controller.CreateAccount(testData);

        //     Assert
        //    Assert.NotNull(result);
        //    var redirectToActionResult = (RedirectToActionResult)result;
        //    Assert.Equal("Index", redirectToActionResult.ActionName);

        //}
    }
}
