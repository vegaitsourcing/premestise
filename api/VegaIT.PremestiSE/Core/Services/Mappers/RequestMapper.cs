using Util;
using System.Linq;
using Core.Interfaces.Models;
using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Entites;
using Core.Services.Mappers.Enums;
namespace Core.Services.Mappers
{
    public class RequestMapper : IMapper<Request, RequestDto>
    {
        public RequestDto DtoFromEntity(Request request)
        {
            return new RequestDto
            {
                Id = HashId.Encode(request.Id),
                Email = request.ParentEmail,
                ParentName = request.ParentName,
                PhoneNumber = request.ParentPhoneNumber,
                ChildName = request.ChildName,
                ChildBirthDate = request.ChildBirthDate,
                Group = request.Group,
                FromKindergardenId = HashId.Encode(request.FromKindergardenId),
                ToKindergardenIds = request.KindergardenWishIds.Select(HashId.Encode).ToList()
            };

        }

        public Request DtoToEntity(RequestDto requestDto)
        {
            return new Request
            {
                ParentEmail = requestDto.Email,
                ParentName = requestDto.ParentName,
                ParentPhoneNumber = requestDto.PhoneNumber,
                ChildName = requestDto.ChildName,
                Group = requestDto.Group,
                ChildBirthDate = requestDto.ChildBirthDate,
                FromKindergardenId = HashId.Decode(requestDto.FromKindergardenId),
                KindergardenWishIds = requestDto.ToKindergardenIds.Select(HashId.Decode).ToList()
            };
        }
    }
}
