using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Password and its confirmation do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
