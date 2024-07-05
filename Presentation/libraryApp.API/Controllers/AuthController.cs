using AutoMapper;
using libraryApp.API.Models.User;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics.Metrics;

namespace libraryApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenLoginAsync(refreshToken);
            return Ok(result);
           
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            LoginResponse result = await _authService.LoginAsync(loginViewModel.UsernameOrEmail,loginViewModel.Password);
            return Ok(result);
        }

    }
}
