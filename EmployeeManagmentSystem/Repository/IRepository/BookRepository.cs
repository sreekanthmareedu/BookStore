using BookStoreAPI.Data;
using BookStoreAPI.Model;

namespace BookStoreAPI.Repository.IRepository
{
    public class BookRepository : Repository<Books>, IBookRepository
    {
        private readonly ApplicationDBContext _db;

        public BookRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Books> UpdateAsync(Books entity)
        {
            
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
