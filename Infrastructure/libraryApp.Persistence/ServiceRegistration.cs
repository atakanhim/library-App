
using Microsoft.Extensions.DependencyInjection;

using libraryApp.Domain.Entities.Identity;
using libraryApp.Persistence.Context;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Persistence.Services;
using libraryApp.Application.Abstractions.Services.Authentications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using libraryApp.Application.Repositories;
using libraryApp.Persistence.Repositories;
using libraryApp.Persistence.Mappings;


namespace libraryApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PersistenceLibraryProfile));




            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;

            }).AddRoles<AppRole>().AddEntityFrameworkStores<LibraryAppDbContext>();
            // repository scopes
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // end of repository scopes

            // entity services
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<INoteService, NoteServices>();
            // end of entity services


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
          
        }

    }
}
