using Microsoft.EntityFrameworkCore;
using Polly;
using Skinet.Application.Evaluator;
using Skinet.Application.Interfaces;
using Skinet.Data.Context;
using Skinet.Entities;

namespace Skinet.Application.Repository;

public class GenericRepository<T>(StoreContext storeContext) : IGenericRepository<T>
    where T : BaseEntity
{
    public void Add(T entity)
    {
        storeContext.Set<T>().Add(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = storeContext.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
        return await query.CountAsync();
    }

    public void Delete(T entity)
    {
        storeContext.Set<T>().Remove(entity);
    }

    public bool Exists(int id)
    {
        return storeContext.Set<T>().Any(x => x.Id == id);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await storeContext
            .Set<T>()
            .Where(x => !x.IsDeleted && x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await storeContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        storeContext.Set<T>().Attach(entity);
        storeContext.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(storeContext.Set<T>().AsQueryable(), spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(
            storeContext.Set<T>().AsQueryable(),
            spec
        );
    }
}
