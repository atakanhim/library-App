
using libraryApp.Domain.Entities;
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
        public DbSet<Book> Books { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<BookHistory> BookHistories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PrivacySettings> PrivacySettings { get; set; }

        // EF CORE TPH KULLANIYOR AYRAC OLARAK Discriminator olusturuyorr
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<BookImageFile> BookImageFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Note>()
               .HasOne(n => n.PrivacySettings)
               .WithOne(ps => ps.Note)
               .HasForeignKey<PrivacySettings>(ps => ps.NoteId);

            modelBuilder.Entity<Shelf>()
                .HasMany(s => s.Books)
                .WithOne(b => b.Shelf)
                .HasForeignKey(b => b.ShelfId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Notes)
                .WithOne(n => n.Book)
                .HasForeignKey(n => n.BookId);

            modelBuilder.Entity<AppUser>()
               .HasMany(b => b.Notes)
               .WithOne(n => n.User)
               .HasForeignKey(n => n.UserId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.History)
                .WithOne(n => n.Book)
                .HasForeignKey(h => h.BookId);



        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : entity üzerinden yapılan degişiklerin yada yeni eklenen vernin yakalanmasını saglayan property. Track edilen verileri yakalar

            var datas = ChangeTracker
                 .Entries<AppUser>();

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

















