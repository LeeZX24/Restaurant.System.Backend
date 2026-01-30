using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.System.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Class & Constructor
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion
        
        #region Get Methods
        public async Task<T> GetAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<List<T>> GetByFieldAsync(Expression<Func<T, bool>> expression) 
        => await _dbSet.Where(expression).ToListAsync();

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();
        #endregion
        
        #region Select Methods
        public async Task<List<T>> SelectAsync(Expression<Func<T, bool>> expression) => await _dbSet.Where(expression).ToListAsync();

        public async Task<List<TResult>> SelectAsync<TResult>(
            Expression<Func<T, bool>> where, 
            Expression<Func<T, TResult>> select)
        {
            return await _dbSet
            .Where(where)
            .Select(select)
            .ToListAsync();   
        }
        #endregion

        #region Add Methods
        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
        }
        #endregion

        #region Update Methods
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
        #endregion

        #region Delete Methods
        public async Task DeleteByFieldAsync(Expression<Func<T, bool>> expression)
        {
            await _dbSet.Where(expression).ExecuteDeleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        #endregion
        
        #region Other Methods
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<long> Count(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).LongCountAsync();
        }
        #endregion

        

        

        

        
    }
}