using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Model
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public int Id{get; set; }

        public string Title { get; set; }


        [ForeignKey("author")]
        public int AuthorID { get; set; }

        public Author author { get; set; }

       
        [ForeignKey("publisher")]
        public int PublisherID{ get; set; }

        public Publisher publisher { get; set; }

        public string ISBN { get; set; }

        public int PublicationYear { get; set; } 



    }
}
