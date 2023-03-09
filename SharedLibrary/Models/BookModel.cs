using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models
{

    public class BookModel
    {
        [Key]

        [Display(Name = "Book ID")]
        [Required]
        public int BookId { get; set; } 
        [Display(Name = "Book Name")]
        [Required]
        public string BookName { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }  //foreign key
        [Display(Name = "Category")]
        
        public string? CategoryName { get; set; }   
        
        public string Author { get; set; }

        public string Publisher { get; set; }

        
        public decimal Price { get; set; }

        //public virtual CategoryModel CategoryModel { get; set; }   //nav prop
    }
}
