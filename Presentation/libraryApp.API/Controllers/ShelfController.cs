using AutoMapper;
using libraryApp.API.Models;
using libraryApp.API.Models.Book;
using libraryApp.API.Models.Shelf;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.User;
using libraryApp.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libraryApp.API.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("[controller]")]
    [ApiController]

    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;
        public ShelfController(IMapper mapper, IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateShelfViewModel vm)
        {
            var response = await _shelfService.CreateShelf(new Application.DTOs.ShelfDTOs.CreateShelfDTO { Id = vm.Id,Name = vm.Name});
            return Ok(response);

        }




    }
}
