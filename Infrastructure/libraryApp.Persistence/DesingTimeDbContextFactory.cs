
using libraryApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace libraryApp.Persistence
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryAppDbContext>
    {
        LibraryAppDbContext IDesignTimeDbContextFactory<LibraryAppDbContext>.CreateDbContext(string[] args)
        {
            //egerki talimat powershell üzerinden geliyorsa ,
            //hangi options parametlerini default olarak kabul etmesi gerektigini belirtiyor.

            DbContextOptionsBuilder<LibraryAppDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new LibraryAppDbContext(dbContextOptionsBuilder.Options); // burda dbconteximizde options verdik
        }
    }
}
