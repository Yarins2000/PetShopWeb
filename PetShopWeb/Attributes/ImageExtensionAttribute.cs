﻿using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.Attributes
{
    /// <summary>
    /// Validation attribute for file's extension.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ImageExtensionAttribute : ValidationAttribute
    {
        /// <summary>
        /// The allowed extensions that recieved from the model.
        /// </summary>
        private readonly string[] extensions;

        public ImageExtensionAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }
        public override bool IsValid(object? value)
        {
            var path = value as string;
            if (path is not null)
            {
                int dotIndex = path.LastIndexOf('.');
                string pathExtension = path[(dotIndex + 1)..].ToLower();
                if (extensions.Contains(pathExtension))
                    return true;
            }
            return false;
        }
    }
}
