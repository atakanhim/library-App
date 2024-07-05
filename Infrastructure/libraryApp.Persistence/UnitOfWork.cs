using libraryApp.Application.Repositories;
using libraryApp.Domain.Entities.Common;
using libraryApp.Domain.Entities.Identity;
using libraryApp.Persistence.Context;
using libraryApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly LibraryAppDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        public IBookRepository BookRepository { get; }

        public UnitOfWork(LibraryAppDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            BookRepository = new BookRepository(_context);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync(); // burda tekrar fırlatmıyoruzki 2 kere rollback olmasın

            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null!;
            }
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }
            var repository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            var datas = _context.ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in datas)
            {

               _ = entity.State switch
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow

                };
            }
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
