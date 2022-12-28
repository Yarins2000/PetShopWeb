using Microsoft.Build.Framework;

namespace PetShopWeb.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
