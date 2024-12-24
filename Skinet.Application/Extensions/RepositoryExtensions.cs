using Microsoft.Extensions.DependencyInjection;
using Skinet.Application.Interfaces;
using Skinet.Application.Repository;

namespace Skinet.Application.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}
