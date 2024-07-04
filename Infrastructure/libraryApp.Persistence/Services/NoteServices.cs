using AutoMapper;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.Abstractions.Storage;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.Repositories;
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
    public class NoteServices : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        readonly UserManager<AppUser> _userManager;


        public NoteServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateNote(CreateNoteDTO dto)
        {
            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();
                var bookModel = await _unitOfWork.GetRepository<Book>().GetAsync(dto.BookId);
                if (bookModel == null)
                    throw new Exception(" Verilen Book Id Yanlis");
                var usermodel = await _userManager.FindByIdAsync(dto.UserId);
                if (usermodel == null)
                    throw new Exception(" Verilen User Id Yanlis");


                Note noteModel = _mapper.Map<CreateNoteDTO, Note>(dto);
                
                await _unitOfWork.GetRepository<Note>().AddAsync(noteModel);

                await _unitOfWork.SaveChangesAsync();
       


                PrivacySettings privacySettings = new PrivacySettings() {
                    Id = Guid.NewGuid().ToString(),
                    NoteId = noteModel.Id                             
                  };

                // Reflection ile dinamik özellik atama
                var propertyInfo = typeof(PrivacySettings).GetProperty(dto.Privacy.ToString());
                if (propertyInfo != null && propertyInfo.CanWrite)             
                    propertyInfo.SetValue(privacySettings, true);               
                else         
                    typeof(PrivacySettings).GetProperty("IsPrivate").SetValue(privacySettings, true);  // Varsayılan olarak IsPrivate ayarlanır



                // bir deger girmez isek is private true geliyor.
                await _unitOfWork.GetRepository<PrivacySettings>().AddAsync(privacySettings);

                await _unitOfWork.SaveChangesAsync();


                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();// 
                throw;
            }
        }

        public Task<IEnumerable<BookDTOIncludeShelf>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
