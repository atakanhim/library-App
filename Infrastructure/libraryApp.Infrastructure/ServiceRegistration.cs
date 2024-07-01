using libraryApp.Application.Abstractions.Token;
using libraryApp.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;


namespace libraryApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
