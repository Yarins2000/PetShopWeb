using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a name")]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string? Name { get; set; }

        public virtual ICollection<Animal>? Animals { get; set; }
    }
}
