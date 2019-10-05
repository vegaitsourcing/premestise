using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using Persistence.Interfaces.Entites;
using Util;

namespace Core.Services.Mappers
{
    public class KindergardenMapper : IMapper<Kindergarden, KindergardenDto>
    {
        public KindergardenDto DtoFromEntity(Kindergarden kindergarden)
        {
            return new KindergardenDto
            {
                Id = EncodeDecode.Encode(kindergarden.Id),
                Name = kindergarden.Name,
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
