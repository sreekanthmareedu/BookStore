using BookStoreAPI.Model;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> Update(Publisher entity);

    }
}
