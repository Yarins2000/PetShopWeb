using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImageExtensionAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] extensions;

        public ImageExtensionAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val-onlyimage", "true");
            //var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            //MergeAttribute(context.Attributes, "data-val-onlyimage", errorMessage);
        }
        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
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
