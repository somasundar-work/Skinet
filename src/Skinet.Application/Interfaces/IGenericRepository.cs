using Skinet.Entities;

namespace Skinet.Application.Interfaces;

public interface IGenericRepository<T>
    where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveAllAsync();
    bool Exists(int id);
}
