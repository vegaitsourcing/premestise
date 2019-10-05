using System;
using System.Collections.Generic;
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
                Id  = EncodeDecode.Encode(request.Id),
                Email = request.ParentEmail,
                ParentName = request.ParentName,
                PhoneNumber =  request.ParentPhoneNumber,
                ChildName = request.ChildName,
                ChildBirthDate = request.ChildBirthDate,
                FromKindergardenId = request.FromKindergardenId
            };
        }

       

        public string Id { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildBirthDate { get; set; }
        public int FromKindergardenId { get; set; }
        public List<int> ToKindergardenIds { get; set; }
    }
}
