using System.Linq.Expressions;

namespace CleanArchitecture.Application.Extensions;

public static class IDbSetExtensions
{
    public static async Task<bool> ExistsAsync<T>(this DbSet<T> dbSet, int id) 
        where T : class
    {
        var entity = await dbSet.FindAsync(id);
        
        return entity is not null;
    }
    
    public static async Task<bool> ExistsAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) 
        where T : class
    {
        return await dbSet.AnyAsync(predicate);
    }
    
    public static async Task<bool> ExistsAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) 
        where T : class
    {
        return await dbSet.AnyAsync(predicate, cancellationToken);
    }
    
    public static async Task<T?> FindByIdAsync<T>(this DbSet<T> dbSet, int id, CancellationToken cancellationToken) 
        where T : class
    {
        return await dbSet.FindAsync([id], cancellationToken);
    }
    
    public static async Task<T?> FindByIdAsync<T>(this DbSet<T> dbSet, int id) 
        where T : class
    {
        return await dbSet.FindAsync([id]);
    }
    
    public static async Task<T?> FindByIdAsync<T>(this DbSet<T> dbSet, Guid id, CancellationToken cancellationToken) 
        where T : class
    {
        return await dbSet.FindAsync([id], cancellationToken);
    }
    
    public static async Task<T?> FindByIdAsync<T>(this DbSet<T> dbSet, Guid id) 
        where T : class
    {
        return await dbSet.FindAsync([id]);
    }
}