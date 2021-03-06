﻿using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Test.WebApplication.Commands.FileDeserializer;

namespace Test.WebApplication.Api.Infrastructure.ValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly FileType[] _extensions;
        public AllowedExtensionsAttribute(params FileType[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (extension != null && !_extensions.Contains(extension.ToFileType()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return "Unknown format";
        }
    }
}
