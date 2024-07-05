using FakeItEasy;
using libraryApp.API.Controllers;
using libraryApp.API.Models.User;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs;
using libraryApp.Application.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace libraryApp.API.Test.Controller
{
    public class AuthControllerTests
    {
        private readonly IAuthService _authService;

        public AuthControllerTests()
        {
            _authService = A.Fake<IAuthService>();
        }

        [Fact]
        public async Task AuthController_Login_ReturnOK()
        {
            // Arrange

            var loginViewModel = A.Fake<LoginViewModel>();
  

            // Mock setup
            A.CallTo(() => _authService.LoginAsync(loginViewModel.UsernameOrEmail, loginViewModel.Password));
                

            var controller = new AuthController(_authService);

            // Act
            var result = await controller.Login(loginViewModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

     
        }
    }
}
