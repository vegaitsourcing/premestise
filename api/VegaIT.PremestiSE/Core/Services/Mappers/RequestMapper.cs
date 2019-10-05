using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using Persistence.Interfaces.Entites;

namespace Core.Services.Mappers
{
    public class RequestMapper : IMapper<Request, RequestDto>
    {
        public RequestDto DtoFromEntity(Request request)
        {
            return new RequestDto
            {
                Id = request.Id,
                Email = request.ParentEmail,
                ParentName = request.ParentName,
                PhoneNumber = request.ParentPhoneNumber,
                ChildName = request.ChildName,
                ChildBirthDate = request.ChildBirthDate,
                FromKindergardenId = request.FromKindergardenId,
                ToKindergardenIds = request.KindergardenWishIds.ToList()
            };

        }

        public Request DtoToEntity(RequestDto requestDto)
        {
            return new Request
            {
                Id = requestDto.Id,
                ParentEmail = requestDto.Email,
                ParentName = requestDto.ParentName,
                ParentPhoneNumber = requestDto.PhoneNumber,
                ChildName = requestDto.ChildName,
                ChildBirthDate = requestDto.ChildBirthDate,
                FromKindergardenId = requestDto.FromKindergardenId,
                KindergardenWishIds = requestDto.ToKindergardenIds.ToArray()
            };
        }
    }
}
