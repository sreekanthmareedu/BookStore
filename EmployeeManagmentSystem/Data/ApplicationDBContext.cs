using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO.BooksDTO;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
     
        public ApplicationDBContext(DbContextOptions options):base(options) { }






        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<User> Users { get; set; }

       
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                Id = 1,
                Name = "Test",
                Email = "Test@gmail.com",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,


            });
                
               
        }



    }
}
