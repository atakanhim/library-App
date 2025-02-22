﻿using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Abstractions.Services
{
    public interface IBooksService
    {
        //Task CreateShelves();
        Task CreateBook(CreateBookDTO createBookDTO);
        Task UploadBookImage(UploadBookImageDto dto);
        Task<IEnumerable<BookDTOIncludeShelf>> GetAllBooks();
        Task<BookDTOIncludeShelf> Get(string id);

        Task RemoveBook(string id);
    }
}
