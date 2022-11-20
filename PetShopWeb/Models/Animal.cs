using PetShopWeb.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWeb.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a name")]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters allowed")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please specify an age")]
        [Range(1, 500, ErrorMessage = "The age must be between 1-500")]
        public int Age { get; set; }

        [ImageExtension(new string[] {"png", "jpg", "jpeg", "webp", "raw", "svg"}, ErrorMessage = "This file extension is not allowed")]
        [Required(ErrorMessage = "Please specify a photo")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Please specify a description")]
        [DataType(DataType.MultilineText)]
        [StringLength(5000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Must choose a category")]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
