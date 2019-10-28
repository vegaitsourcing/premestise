using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using Persistence.Interfaces.Entites;
using System;
using Util;

namespace Core.Services.Mappers
{
    public class KindergardenMapper : IMapper<Kindergarden, KindergardenDto>
    {
        public KindergardenDto DtoFromEntity(Kindergarden kindergarden)
        {
            return new KindergardenDto
            {
                Id = HashId.Encode(kindergarden.Id),
                Name = $"{kindergarden.Name} - {kindergarden.Street} {kindergarden.StreetNumber}, {kindergarden.City}",
                Longitude = kindergarden.Longitude,
                Latitude = kindergarden.Latitude
            };
        }

        public Kindergarden DtoToEntity(KindergardenDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
