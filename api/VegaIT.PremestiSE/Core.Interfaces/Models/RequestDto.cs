using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Persistence.Interfaces.Entites;
using Util;

namespace Core.Interfaces.Models
{
    public class RequestDto
    {


        public static RequestDto FromRequestEntity(Request request)
        {

            return new RequestDto()
            {
                Id = EncodeDecode.Encode(request.Id),
                Email = request.ParentEmail,
                ParentName = request.ParentName,
                PhoneNumber = request.ParentPhoneNumber,
                ChildName = request.ChildName,
                ChildBirthDate = request.ChildBirthDate,
                FromKindergardenId = EncodeDecode.Encode(request.FromKindergardenId)
            };
        }


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
        [Required]
        [DataType(DataType.Text, ErrorMessage = "You must enter text")]
        public string ChildName { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please check your date format")]
        public DateTime ChildBirthDate { get; set; }
        [Required]
        [DataType(DataType.Text, ErrorMessage = "You must enter text")]
        public string FromKindergardenId { get; set; }
        [Required]
        [MinLength(1)]
        public List<string> ToKindergardenIds { get; set; }
    }
}
