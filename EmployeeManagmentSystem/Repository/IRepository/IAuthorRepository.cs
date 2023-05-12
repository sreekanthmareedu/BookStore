using BookStoreAPI.Model;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {

        Task<Author> Update(Author entity);
    }
}
