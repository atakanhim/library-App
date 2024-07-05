using AutoMapper;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.Abstractions.Storage;
using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.Repositories;
using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entities = libraryApp.Domain.Entities;

namespace libraryApp.Persistence.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        IStorageService _storageService;


        public BooksService(IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService;
        }

    
        public async Task CreateBook(CreateBookDTO createBookDTO)
        {
            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();

                Book bookModel = _mapper.Map<CreateBookDTO, Book>(createBookDTO);

                await _unitOfWork.GetRepository<Book>().AddAsync(bookModel);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();// 
                throw;
            }
        }

        public async Task<IEnumerable<BookDTOIncludeShelf>> GetAllBooks()
        {
            try
            {
                IEnumerable<Book> books = await _unitOfWork.BookRepository.GetAllBooksWithShelf();

                IEnumerable<BookDTOIncludeShelf> bookModel = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTOIncludeShelf>>(books);

                return bookModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveBook(string id)
        {
            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();

                if (id == null)
                    throw new ArgumentNullException("id");

                var book = await _unitOfWork.GetRepository<Book>().GetAsync(id);

                if (book == null)
                    throw new Exception("book bulunamadı");

                _unitOfWork.GetRepository<Book>().Remove(book);
                await _unitOfWork.SaveChangesAsync();


                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();// 
                throw;
            }
        }

        public async Task UploadBookImage(UploadBookImageDto dto)
        {
            var sooooooo = dto;

            try
            {
                using var transaction = _unitOfWork.BeginTransactionAsync();
                var bookModel = await _unitOfWork.GetRepository<Book>().GetAsync(dto.Id);
                if (bookModel == null)
                    throw new Exception(" Verilen Book Id Yanlis");

                var model = await _storageService.UploadAsync("photo-images", dto.Files); // locale upload ediyoruz ve bize 2 tane deger donuyor filename ve path              
                await _unitOfWork.GetRepository<BookImageFile>().AddRangeAsync(model.Select(d => new BookImageFile()
                {
                    Id = Guid.NewGuid().ToString(),             
                    FileName = d.filename,
                    Path = d.pathOrContainer,
                    Storage = _storageService.StorageName,
                    BookId = bookModel.Id,
                    

                }).ToList());

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
