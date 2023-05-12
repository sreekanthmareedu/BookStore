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
        public Task<Publisher> Update(Publisher entity)
        {
            throw new NotImplementedException();
        }
    }
}
