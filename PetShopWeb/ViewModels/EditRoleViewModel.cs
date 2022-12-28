using System.ComponentModel.DataAnnotations;
using System.Security;

namespace PetShopWeb.ViewModels
{
    public class EditRoleViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name = "Role name")]
        public string? RoleName { get; set; }
        public List<string>? Users { get; set; }

        public EditRoleViewModel()
        {
            Users = new();
        }
    }
}
