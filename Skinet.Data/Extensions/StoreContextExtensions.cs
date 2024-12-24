using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Data.Context;
using Skinet.Data.Seed;

namespace Skinet.Data.Extensions;

/// <summary>
/// Provides extension methods for the StoreContext.
/// </summary>
public static class StoreContextExtensions
{
    /// <summary>
    /// Migrates the store database synchronously.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    public static void MigrateStore(this WebApplication app)
    {
        try
        {
            MigrateStoreAsync(app).Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    /// <summary>
    /// Migrates the store database asynchronously.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task MigrateStoreAsync(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    /// <summary>
    /// Adds the StoreContext to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddStoreContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<StoreContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString(Constants.ContextConstants.DefaultConnection)
            );
        });

        return services;
    }
}
