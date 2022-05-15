using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace PAMS.Domain.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateLogoAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">                The value to validate. </param>
        /// <param name="validationContext">    The context information about the validation operation. </param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" />
        /// class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IFormFile file)) // Logo is not compulsory
                return ValidationResult.Success;

            var configuration = (IConfiguration)validationContext.GetService(typeof(IConfiguration));
            var maxLogoSize = configuration.GetValue<int>("MaxLogoSize");
            var maxSize = (maxLogoSize / 1000.0) * 1024 * 1024; // e.g maxSize 40 ----> 0.04 * 1024 * 1024 = 40kb

            if (file.Length > maxSize)
                return new ValidationResult($"Logo file size must not exceed {maxLogoSize}kb");

            var fileStorageService = (IFileStorageService)validationContext.GetService(typeof(IFileStorageService));

            if (fileStorageService?.ValidateFile(new[] { "jpg", "png", "jpeg" }, file.FileName) ?? false)
                return ValidationResult.Success;

            return new ValidationResult("Logo file type must be .jpg or .png or .jpeg");
        }
    }
}
