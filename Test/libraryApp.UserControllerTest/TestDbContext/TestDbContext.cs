using libraryApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.API.Test.TestDbContext
{
    public class TestDbContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
    }
}
