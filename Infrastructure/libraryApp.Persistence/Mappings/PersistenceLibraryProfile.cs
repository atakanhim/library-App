using AutoMapper;
using libraryApp.Application.DTOs.Book;
using libraryApp.Application.DTOs.Shelf;
using libraryApp.Domain.Entities;


namespace libraryApp.Persistence.Mappings
{
    public class PersistenceLibraryProfile : Profile
    {
        public PersistenceLibraryProfile()
        {
            CreateMap<CreateBookDTO, Book>().ReverseMap();
            CreateMap<BookDTOIncludeShelf, Book>().ReverseMap();
            CreateMap<BaseBookDTO, Book>().ReverseMap();



            CreateMap<BaseShelfDTO, Shelf>().ReverseMap();
            CreateMap<ShelfDTOIncludeNothing, Shelf>().ReverseMap();
   
   
        }
    }
}
