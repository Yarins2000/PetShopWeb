using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWeb.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.MultilineText), MaxLength(300)]
        [Required(ErrorMessage = "Please specify a comment")]
        public string? CommentText { get; set; }

        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }
    }
}
