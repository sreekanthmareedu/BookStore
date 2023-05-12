using BookStoreAPI.Data;
using BookStoreAPI.Model;
using BookStoreAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace BookStoreAPI.Repository
{
    public class AuthorRepository : IRepository<Author>, IAuthorRepository
    {

        private readonly ApplicationDBContext _db;

        public AuthorRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public Task UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
