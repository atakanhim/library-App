
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


    }
 }

















