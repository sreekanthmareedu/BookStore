using BookStoreAPI.Data;
using BookStoreAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BookStoreAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;

        internal DbSet<T> dbSet;
        public Repository(ApplicationDBContext db)
        {

            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
            
          
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
             dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
             await _db.SaveChangesAsync();
        }

       /* public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }*/
    }
}
