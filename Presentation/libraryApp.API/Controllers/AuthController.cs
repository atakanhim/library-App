using AutoMapper;
using libraryApp.API.Models;
using libraryApp.Application.Abstractions.Services;
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
        private IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin()
        {
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var result = await _authService.LoginAsync(loginViewModel.usernameOrEmail,loginViewModel.password);
            return Ok(result);
        }

    }
}
