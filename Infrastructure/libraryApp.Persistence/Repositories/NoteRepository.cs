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
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {
        public NoteRepository(LibraryAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await _dbSet.Include(x => x.Book).Include(y => y.User).Include(y => y.PrivacySettings).ToListAsync();
        }

        public async Task<Note> GetNote(string id)
        {
            return await _dbSet.Include(x => x.Book).Include(y=>y.User).Include(y => y.PrivacySettings).FirstOrDefaultAsync();
        }
    }
}
