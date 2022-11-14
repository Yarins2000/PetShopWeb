using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWeb.Models
{
    public class Animal
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a name")]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please specify an age")]
        [Range(0, 500)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please specify an URL")]
        [Url, MaxLength(100)]
        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Please specify a description")]
        [DataType(DataType.MultilineText)]
        //[MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Must choose a category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
