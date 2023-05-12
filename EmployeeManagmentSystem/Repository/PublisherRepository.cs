using BookStoreAPI.Data;
using BookStoreAPI.Model;
using BookStoreAPI.Repository.IRepository;

namespace BookStoreAPI.Repository
{
    public class PublisherRepository : Repository<Publisher>,IPublisherRepository
    {

        private readonly ApplicationDBContext _db;

        public PublisherRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Publisher> UpdateAsync(Publisher entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
