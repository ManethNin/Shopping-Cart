using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? BookName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? AuthorName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        
        [Required]
        [NotMapped]
        public int Quantity { get; set; }


    }
}
