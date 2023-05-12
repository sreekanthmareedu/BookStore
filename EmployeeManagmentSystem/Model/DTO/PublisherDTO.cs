using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        
        public string Email { get; set; }

    }
}
