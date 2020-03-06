using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Persistence.Interfaces.Entites;
using Util;
using Util.Enums;
namespace Core.Interfaces.Models
{


    public class RequestDto
    {


        [Key]
        public string Id { get; set; }


        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Check you email format")]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Text, ErrorMessage = "You must enter text")]
        public string ParentName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "You must enter numeric values")]
        public string PhoneNumber { get; set; }

        public string ChildName { get; set; }
        public DateTime ChildBirthDate { get; set; }
        public string City { get; set; }

        public AgeGroup Group { get; set; }
        
        [Required]
        [DataType(DataType.Text, ErrorMessage = "You must enter text")]
        public string FromKindergardenId { get; set; }
        [Required]
        [MinLength(1)]
        public List<string> ToKindergardenIds { get; set; }
    }
}
