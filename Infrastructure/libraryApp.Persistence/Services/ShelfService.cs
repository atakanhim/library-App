using AutoMapper;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.ShelfDTOs;
using libraryApp.Application.Repositories;
using libraryApp.Application.ResponseModels;
using libraryApp.Domain.Entities;
using libraryApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Persistence.Services
{
    public class ShelfService : IShelfService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;


        public ShelfService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async  Task<CreateShelfResponse> CreateShelf(CreateShelfDTO dto)
        {
            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();
                var shelfModel = new Shelf()
                {
                    Id = dto.Id,
                   Name = dto.Name,
                   
                };

                await _unitOfWork.GetRepository<Shelf>().AddAsync(shelfModel);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return new()
                {
                    ShelfId = dto.Id,
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();// 
                throw;
            }
        }

       
    }
}
