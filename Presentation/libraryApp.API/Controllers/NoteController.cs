using AutoMapper;
using libraryApp.API.Models;
using libraryApp.API.Models.Book;
using libraryApp.API.Models.Note;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.User;
using libraryApp.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var model = await _service.GetAll();
            return Ok(model);

        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> Get([FromRoute] string Id)
        {
            var model = await _service.Get(Id);
            return Ok(model);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateNoteViewModel vm)
        {
            CreateNoteDTO dto = _mapper.Map<CreateNoteViewModel, CreateNoteDTO>(vm);
            await _service.CreateNote(dto);
            return Ok();

        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove([FromQuery] string NoteId)
        {
            await _service.RemoveNote(NoteId);
            return Ok();

        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateNoteViewModel vm)
        {
            if(vm.Content.IsNullOrEmpty() && !vm.Privacy.HasValue) 
                throw new Exception("En az 1 deger update etmelisiniz.");
            
            UpdateNoteDTO dto = _mapper.Map<UpdateNoteViewModel, UpdateNoteDTO>(vm);
            await _service.Update(dto);
            return Ok();

        }




    }
}
