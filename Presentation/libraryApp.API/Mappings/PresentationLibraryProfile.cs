using AutoMapper;
using libraryApp.API.Models.Book;
using libraryApp.API.Models.Note;
using libraryApp.API.Models.User;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Mappings
{
    public class PresentationLibraryProfile : Profile
    {
        public PresentationLibraryProfile()
        {
            //user
            CreateMap<CreateUserViewModel, CreateUserDTO>().ReverseMap();
            CreateMap<UpdateUserViewModel, UpdateUserDTO>().ReverseMap();

            // book
            CreateMap<CreateBookViewModel, CreateBookDTO>().ReverseMap();        
            
            
            
            // note
            CreateMap<CreateNoteViewModel, CreateNoteDTO>().ReverseMap();
            CreateMap<UpdateNoteViewModel, UpdateNoteDTO>().ReverseMap();
  
   
        }
    }
}
