﻿using AutoMapper;
using libraryApp.API.Models;
using libraryApp.API.Models.Book;
using libraryApp.Application.DTOs.Book;
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
            CreateMap<CreateUserViewModel, CreateUserDTO>().ReverseMap();
            CreateMap<UpdateUserViewModel, UpdateUserDTO>().ReverseMap();

            CreateMap<CreateBookViewModel, CreateBookDTO>().ReverseMap();
   
   
        }
    }
}
