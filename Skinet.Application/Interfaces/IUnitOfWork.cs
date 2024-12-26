using System;
using Skinet.Entities;

namespace Skinet.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity;
    Task<bool> Complete();
}
