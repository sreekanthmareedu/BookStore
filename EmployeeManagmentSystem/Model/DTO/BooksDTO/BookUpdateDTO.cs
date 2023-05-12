using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Model.DTO.BooksDTO
{
    public class BookUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }


        [Required]
        public int Author { get; set; }


        [Required]
        public int Publisher { get; set; }

        public string ISBN { get; set; }

        public int PublicationYear { get; set; }



    }
}
