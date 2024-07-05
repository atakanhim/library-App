using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.ShelfDTOs;
using libraryApp.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Abstractions.Services
{
    public interface IShelfService
    {
        Task<CreateShelfResponse> CreateShelf(CreateShelfDTO dto);
    }
}
