using BookStoreAPI.Model;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IBookRepository : IRepository<Books>
    {
        public Task<Books> UpdateAsync(Books entity);
    }
}
