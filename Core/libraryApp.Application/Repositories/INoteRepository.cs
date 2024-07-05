using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Repositories
{
    public interface INoteRepository: IRepository<Note>
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
    }
}
