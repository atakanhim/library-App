using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Abstractions.Services
{
    public interface INoteService
    {
        Task<IEnumerable<BookDTOIncludeShelf>> GetAll();
        Task CreateNote(CreateNoteDTO dto);// admin note olusturabilir
    }
}
