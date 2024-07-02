using libraryApp.Infrastructure;
using libraryApp.Persistence;
using libraryApp.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using libraryApp.Domain.Entities.Identity;
using libraryApp.API.Extensions;
using libraryApp.Application.Mappings;
using FluentValidation.AspNetCore;
using libraryApp.Infrastructure.Filters;
using Serilog;
using Serilog.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
            
        };
    });
builder.Services.AddAutoMapper(typeof(PresentationLibraryProfile));
builder.Services.AddAuthorization();
//log ekleme

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();

});
// log bitis

// validation ekleme 
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).
    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
// validation bitis 

builder.Services.AddDbContext<LibraryAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// log

// Middleware veya controller içinde loglama yaparken:
app.Use(async (context, next) =>
{
    // LogContext kullanarak ek bilgiler ekleyin
    using (LogContext.PushProperty("Action", context.Request.Path))
    using (LogContext.PushProperty("UserName", context.User.Identity.Name ?? "Anonymous"))
    {
        await next.Invoke();
    }
});
// Global exception handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        using (LogContext.PushProperty("Action", context.Request.Path))
        using (LogContext.PushProperty("UserName", context.User.Identity?.Name ?? "Anonymous"))
        {
            logger.LogError(ex, "An unhandled exception has occurred.");
        }
        throw; // Ýstisnayý tekrar fýrlat ki diðer middleware'ler de haberdar olsun
    }
});

// log bitis



// Uygulamanýn geri kalaný burada


// log middleware
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());


app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
{
    options.AllowAnyOrigin(); // Tüm kaynaklara eriþime izin verir
    options.AllowAnyMethod(); // Tüm HTTP metodlarýna izin verir
    options.AllowAnyHeader(); // Tüm baþlýklara izin verir
});
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();// yetkilendirme

app.MapControllers();

app.Run();
