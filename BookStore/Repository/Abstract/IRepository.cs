using System.Linq.Expressions;

namespace BookStore.Repository.Abstract;

public interface IRepository<T>  where T : class
{
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<ICollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes);
}