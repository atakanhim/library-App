using AutoMapper;
using libraryApp.API.Models;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics.Metrics;

namespace libraryApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel viewModel) // daha sonradan viewmodel eklenecek
        {
            CreateUserDTO createUserDTO = _mapper.Map<CreateUserViewModel, CreateUserDTO>(viewModel);

            var result = await _userService.CreateAsync(createUserDTO);
            return Ok(result);
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
