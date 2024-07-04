using libraryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
         IBookRepository BookRepository { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

    }
}
