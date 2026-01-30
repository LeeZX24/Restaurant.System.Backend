using System.Linq.Expressions;

namespace Restaurant.System.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetByFieldAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> SelectAsync(Expression<Func<T, bool>> expression);
        Task<List<TResult>> SelectAsync<TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteByFieldAsync(Expression<Func<T, bool>> expression);
        Task SaveChangesAsync();
        Task<long> Count(Expression<Func<T, bool>> expression);
    }
}