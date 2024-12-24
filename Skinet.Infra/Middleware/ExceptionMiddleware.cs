using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Skinet.Infra.Error;

namespace Skinet.Infra.Middleware;

public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(env, context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        IHostEnvironment env,
        HttpContext context,
        Exception ex
    )
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = env.IsDevelopment()
            ? new ApiErrorResponse(
                context.Response.StatusCode,
                ex.Message,
                ex.StackTrace?.ToString()
            )
            : new ApiErrorResponse(
                context.Response.StatusCode,
                "Internal Server Error",
                ex.Message
            );

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
