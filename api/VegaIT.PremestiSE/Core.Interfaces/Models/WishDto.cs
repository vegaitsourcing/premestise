using System;
using System.Collections.Generic;

namespace Core.Interfaces.Models
{
    public class WishDto
    {
        public int RequestId { get; set; }
        public DateTime ChildBirthDate { get; set; }
        public KindergardenDto FromKindergarden { get; set; }
        public IEnumerable<KindergardenDto> ToKindergardens { get; set; }
        
    }
}