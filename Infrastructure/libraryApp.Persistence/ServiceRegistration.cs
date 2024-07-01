
using Microsoft.Extensions.DependencyInjection;

using libraryApp.Domain.Entities.Identity;
using libraryApp.Persistence.Context;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Persistence.Services;
using libraryApp.Application.Abstractions.Services.Authentications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace libraryApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {

            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;

            }).AddRoles<AppRole>().AddEntityFrameworkStores<LibraryAppDbContext>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
          
        }

    }
}
