using AutoMapper;
using libraryApp.Application.DTOs.Book;
using libraryApp.Domain.Entities;


namespace libraryApp.Persistence.Mappings
{
    public class PersistenceLibraryProfile : Profile
    {
        public PersistenceLibraryProfile()
        {
            CreateMap<CreateBookDTO, Book>().ReverseMap();
   
   
        }
    }
}
