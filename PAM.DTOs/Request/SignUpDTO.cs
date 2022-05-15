using Microsoft.AspNetCore.Http;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class SignUpDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(4,ErrorMessage ="Activation code must be upto four digits!")]
        public string ActivationCode { get; set; } 
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }

        [ValidateLogo] public IFormFile Picture { get; set; }
    }

    public class EditDTO
    {
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        [ValidateLogo] public IFormFile Picture { get; set; }
    }
}
