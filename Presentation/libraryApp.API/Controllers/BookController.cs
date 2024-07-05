using AutoMapper;
using libraryApp.API.Models;
using libraryApp.API.Models.Book;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libraryApp.API.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("[controller]")]
    [ApiController]

    public class BookController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private IMapper _mapper;
        public BookController(IMapper mapper, IBooksService booksService)
        {
            _mapper = mapper;
           _booksService = booksService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var model = await _booksService.GetAllBooks();
            return Ok(model);

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateBookViewModel createBookViewModel)
        {
            CreateBookDTO createBookDTO = _mapper.Map<CreateBookViewModel, CreateBookDTO>(createBookViewModel);
            await _booksService.CreateBook(createBookDTO);
            return Ok();

        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove([FromQuery] string BookId)
        {
            await _booksService.RemoveBook(BookId);
            return Ok();

        }
        [HttpPut("[action]")] // bu id bildirmedik 
        public async Task<IActionResult> Upload([FromQuery] UploadBookImageViewModel viewModel)
        {

            viewModel.Files = Request.Form.Files;
            await _booksService.UploadBookImage(new UploadBookImageDto
            {
                Id = viewModel.Id,
                Files = viewModel.Files
            });
            return Ok();
        }
 


        //[HttpPost("[action]")]
        //public async Task<IActionResult> CreateShelves()
        //{
        //    await _booksService.CreateShelves();
        //    return Ok();

        //}
    }
}
