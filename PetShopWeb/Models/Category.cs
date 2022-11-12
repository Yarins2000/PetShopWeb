using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWeb.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a name")]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string? Name { get; set; }

        public virtual ICollection<Animal>? Animals { get; set; }
    }
}
