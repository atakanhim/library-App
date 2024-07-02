﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Serilog.Context;

namespace libraryApp.API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;// "application/json"

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>(); // hata getiriyor
                    if (contextFeature != null)
                    {                      
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                           // LongError = contextFeature.Error.ToString(),
                            Title = "Hata alındı!"
                        })); ;
                    }
                });
            });
        }
    }
}
