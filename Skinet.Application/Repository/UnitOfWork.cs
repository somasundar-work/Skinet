using System;
using System.Collections.Concurrent;
using Skinet.Application.Interfaces;
using Skinet.Data.Context;
using Skinet.Entities;

namespace Skinet.Application.Repository;

public class UnitOfWork(StoreContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)
            _repositories.GetOrAdd(
                type,
                t =>
                {
                    var repoType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));
                    return Activator.CreateInstance(repoType, context)
                        ?? throw new InvalidOperationException(
                            $"Could not create repository instance for {t}"
                        );
                }
            );
    }
}
