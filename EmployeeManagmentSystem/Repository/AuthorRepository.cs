using BookStoreAPI.Data;
using BookStoreAPI.Model;
using BookStoreAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace BookStoreAPI.Repository
{
    public class AuthorRepository : Repository<Author>,IAuthorRepository
    {

        private readonly ApplicationDBContext _db;

        public AuthorRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
