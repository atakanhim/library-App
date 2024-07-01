
using libraryApp.Domain.Entities.Common;
using libraryApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace libraryApp.Persistence.Context
{
    public class LibraryAppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public LibraryAppDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : entity üzerinden yapılan degişiklerin yada yeni eklenen vernin yakalanmasını saglayan property. Track edilen verileri yakalar

            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var entity in datas)
            {

                _ = entity.State switch
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow

                };


            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
 }

















