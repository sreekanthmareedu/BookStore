using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Model.DTO.BooksDTO
{
    public class BookCreateDTO
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        public int authors { get; set; }

       


        [Required]
        public int publishers { get; set; }

        
        public string ISBN { get; set; }

        public int PublicationYear { get; set; }



    }
}
