using BookStoreAPI.Model;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> UpdateAsync(Publisher entity);

    }
}
