using AutoMapper;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.Abstractions.Storage;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.Repositories;
using libraryApp.Domain.Entities;
using libraryApp.Domain.Entities.Identity;
using libraryApp.Persistence.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

        public async  Task<IEnumerable<ListNoteDTO>> GetAll()
        {
            try
            {
                IEnumerable<Note> notes = await _unitOfWork.NoteRepository.GetAllNotes();
                if (notes == null)
                    throw new Exception("note Bulunamadı");


                IEnumerable<ListNoteDTO> notesModel = _mapper.Map<IEnumerable<Note>, IEnumerable<ListNoteDTO>>(notes);

                return notesModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ListNoteDTO> Get(string id)
        {
            try
            {
                Note note = await _unitOfWork.NoteRepository.GetNote(id);
                if (note == null)
                    throw new Exception("note Bulunamadı");


                ListNoteDTO noteModel = _mapper.Map<Note, ListNoteDTO>(note);

                return noteModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(UpdateNoteDTO noteDto)
        {
            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();

                var noteModel = await _unitOfWork.GetRepository<Note>().GetAsync(noteDto.Id);
                if (noteModel == null)
                    throw new Exception(" Verilen Note Id Yanlis");


                var privacyModel = await _unitOfWork.GetRepository<PrivacySettings>().GetEntityAsync(privacy => privacy.NoteId == noteDto.Id);
                if (privacyModel == null)
                {
                    // update edilecek notun privacy degeri yoksa olusturoyruz
                    PrivacySettings privacySettings = new PrivacySettings()
                    {
                        Id = Guid.NewGuid().ToString(),
                        NoteId = noteModel.Id
                    };

                    // Reflection ile dinamik özellik atama
                    var propertyInfo = typeof(PrivacySettings).GetProperty(noteDto.Privacy.ToString());
                    if (propertyInfo != null && propertyInfo.CanWrite)
                        propertyInfo.SetValue(privacySettings, true);
                    else
                        typeof(PrivacySettings).GetProperty("IsPrivate").SetValue(privacySettings, true);

                    // bir deger girmez isek is private true geliyor.
                    await _unitOfWork.GetRepository<PrivacySettings>().AddAsync(privacySettings);

                    await _unitOfWork.SaveChangesAsync();
                }
                else 
                {
                    // ever privacy deger veritabanında var ise 
                    if (noteDto.Privacy != null && Enum.IsDefined(typeof(PrivacySettingEnum), noteDto.Privacy))
                    {
                        var propertyInfo = typeof(PrivacySettings).GetProperty(noteDto.Privacy.ToString());
                        if (propertyInfo != null && propertyInfo.CanWrite)
                            propertyInfo.SetValue(privacyModel, true); // brda degistiyoruz
                        else
                            typeof(PrivacySettings).GetProperty("IsPrivate").SetValue(privacyModel, true);

                    }

                 
                }


                if (!noteDto.Content.IsNullOrEmpty())
                noteModel.Content = noteDto.Content;

                 // track edilen degeleri save ediyoruz
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();// 
                throw;
            }
        }

        public async Task RemoveNote(string id)
        {
            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();

                if (id == null)
                    throw new ArgumentNullException("id");

                var note = await _unitOfWork.GetRepository<Note>().GetAsync(id);

                if (note == null)
                    throw new Exception("note bulunamadı");

                _unitOfWork.GetRepository<Note>().Remove(note);
                await _unitOfWork.SaveChangesAsync();


                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();// 
                throw;
            }
        }
    }
}
