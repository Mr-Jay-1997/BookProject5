using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Models
{
    public class CategoryModel
    {
        [Key]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }
    }
}
