using Microsoft.AspNetCore.Builder;
using Skinet.Infra.Middleware;

namespace Skinet.Infra.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
