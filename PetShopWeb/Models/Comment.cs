using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWeb.Models
{
    public class Comment
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a comment")]
        [DataType(DataType.MultilineText), MaxLength(100)]
        public string? CommentText { get; set; }

        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }
    }
}
