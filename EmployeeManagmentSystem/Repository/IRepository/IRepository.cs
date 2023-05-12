using System.Linq.Expressions;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IRepository<T>  where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveAsync();
    }
}
