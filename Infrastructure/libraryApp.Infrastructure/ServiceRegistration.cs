using libraryApp.Application.Abstractions.Storage;
using libraryApp.Application.Abstractions.Token;
using libraryApp.Infrastructure.Services.Storage;
using libraryApp.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;


namespace libraryApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IStorageService, StorageService>();

        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage//istorageden türemiş bir class olması gerekiyor
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
