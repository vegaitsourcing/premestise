using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Models
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildBirthDate { get; set; }
        public KindergardenDto FromKindergarden { get; set; }
        public List<KindergardenDto> ToKindergardenDtos { get; set; }
    }
}
