using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Repositories
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByISBN(string isbn);
        Task<IEnumerable<Book>> GetAllBooksWithShelf();
    }
}
