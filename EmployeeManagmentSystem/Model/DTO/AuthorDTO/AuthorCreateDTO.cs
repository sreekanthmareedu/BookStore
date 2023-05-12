using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model.DTO
{
    public class AuthorCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
