using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Services.Mappers;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace Core.Services
{
    public class KindergardenService : IKindergardenService
    {
        private readonly IKindergardenRepository _kindergardenRepository;

        public KindergardenService(IKindergardenRepository kindergardenRepository)
        {
            _kindergardenRepository = kindergardenRepository;
        }

        public IEnumerable<KindergardenDto> GetAll()
        {
            var kindergardens = _kindergardenRepository.GetAll()
                                                       .Select(new KindergardenMapper().DtoFromEntity);

            return kindergardens;
        }
    }
}
