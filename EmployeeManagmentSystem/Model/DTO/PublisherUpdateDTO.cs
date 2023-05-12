using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model.DTO
{
    public class PublisherUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
