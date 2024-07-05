using libraryApp.Application.Repositories;
using libraryApp.Domain.Entities;
using libraryApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Persistence.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksByISBN(string isbn)
        {
            return await _dbSet.Where(p=>p.ISBN == isbn).ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAllBooksWithShelf()
        {
            return await _dbSet.Include(x=>x.Shelf).ToListAsync();
        }

        public async Task<Book> GetBookWithShelf()
        {
            return await _dbSet.Include(x => x.Shelf).FirstOrDefaultAsync();
        }
    }
}
