using AutoMapper;
using libraryApp.API.Models;
using libraryApp.API.Models.Book;
using libraryApp.API.Models.Note;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libraryApp.API.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("[controller]")]
    [ApiController]

    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;
        private IMapper _mapper;
        public NoteController(IMapper mapper, INoteService noteservice)
        {
            _mapper = mapper;
           _service = noteservice;
        }
      
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateNoteViewModel vm)
        {
            CreateNoteDTO dto = _mapper.Map<CreateNoteViewModel, CreateNoteDTO>(vm);
            await _service.CreateNote(dto);
            return Ok();

        }
 
 


   
    }
}
