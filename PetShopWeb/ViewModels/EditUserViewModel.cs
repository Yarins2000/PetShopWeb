﻿using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public List<string>? Claims { get; set; }
        public List<string>? Roles { get; set; }

        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
    }
}
