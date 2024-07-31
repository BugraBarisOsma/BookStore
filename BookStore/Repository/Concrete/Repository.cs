


using System.Linq.Expressions;
using BookStore.Context;
using BookStore.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.Concrete;

public class Repository<T> : IRepository<T> where T : class
{
private readonly AppDbContext _context;


public Repository(AppDbContext context)
{
    _context = context;
 
}

public async Task<T> GetByIdAsync(int id)
{
    return await _context.Set<T>().FindAsync(id);
}

public async Task<ICollection<T>> GetAllAsync()
{
    return await _context.Set<T>().ToListAsync();
}

public async Task AddAsync(T entity)
{
    await _context.Set<T>().AddAsync(entity);
}

public async Task UpdateAsync(T entity)
{
    _context.Set<T>().Update(entity);
}

public async Task DeleteAsync(int id)
{
    var entity = await GetByIdAsync(id);
    if (entity != null)
    {
        _context.Set<T>().Remove(entity);
    }
}

public async Task<ICollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
{
    return await _context.Set<T>().Where(predicate).ToListAsync();
}

public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
{
    return await _context.Set<T>().FirstOrDefaultAsync(predicate);
}

public async Task<ICollection<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes)
{
    IQueryable<T> query = _context.Set<T>();
    foreach (var include in includes)
    {
        query = query.Include(include);
    }
    return await query.ToListAsync();
}
}