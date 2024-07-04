using AutoMapper;
using libraryApp.API.Models.User;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using System.Diagnostics.Metrics;

namespace libraryApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel viewModel) // daha sonradan viewmodel eklenecek
        {
            CreateUserDTO createUserDTO = _mapper.Map<CreateUserViewModel, CreateUserDTO>(viewModel);    
            var result = await _userService.CreateAsync(createUserDTO);
            return Ok(result);
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel viewModel) // daha sonradan viewmodel eklenecek
        {
            UpdateUserDTO updateUserDto = _mapper.Map<UpdateUserViewModel, UpdateUserDTO>(viewModel);
             await _userService.UpdateAsync(updateUserDto);

            return Ok();
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> AdminControl()
        {

       
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserControl()
        {
            return Ok();
        }

    }
}
