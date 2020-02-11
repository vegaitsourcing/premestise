using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Persistence.Interfaces.Entites;
using Util;

namespace Core.Interfaces.Models
{
    public class ContactFormDto
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Check you email format")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text, ErrorMessage = "You must enter text")]
        public string Message { get; set; }
    }
}
