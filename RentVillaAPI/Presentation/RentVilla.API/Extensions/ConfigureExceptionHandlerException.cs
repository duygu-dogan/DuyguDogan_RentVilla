using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace RentVilla.API.Extensions
{
    static public class ConfigureExceptionHandlerException
    {
        public static void ConfigureExceptionHandler<T>(this IApplicationBuilder app, ILogger<T> logger)
        {
            app.UseExceptionHandler(builder => 
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error.Message}");
                        await context.Response.WriteAsync(JsonSerializer.Serialize(
                            new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                                Title = "An error occurred"
                            }));
                    }

                }));
        }
    }
}
