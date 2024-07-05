using AutoMapper;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.PrivacySettingsDTOs;
using libraryApp.Application.DTOs.ShelfDTOs;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities;
using libraryApp.Domain.Entities.Identity;


namespace libraryApp.Persistence.Mappings
{
    public class PersistenceLibraryProfile : Profile
    {
        public PersistenceLibraryProfile()
        {
            CreateMap<CreateBookDTO, Book>().ReverseMap();
            CreateMap<BookDTOIncludeShelf, Book>().ReverseMap();
            CreateMap<BookDTOIncludeNothing, Book>().ReverseMap();
            CreateMap<BaseBookDTO, Book>().ReverseMap();


            CreateMap<BaseShelfDTO, Shelf>().ReverseMap();
            CreateMap<ShelfDTOIncludeNothing, Shelf>().ReverseMap();


            CreateMap<DefaultPrivacySettingsDTOInclude, PrivacySettings>().ReverseMap();



            CreateMap<UserDTO, AppUser>().ReverseMap();
            CreateMap<ListUserDTO, AppUser>().ReverseMap();


            CreateMap<NoteDTOIncludeNothing, Note>().ReverseMap();
            CreateMap<BaseNoteDTO, Note>().ReverseMap();
            CreateMap<UpdateNoteDTO, Note>().ReverseMap();
            CreateMap<CreateNoteDTO, Note>().ReverseMap();
            CreateMap<ListNoteDTO, Note>().ReverseMap();





   


   
        }
    }
}
