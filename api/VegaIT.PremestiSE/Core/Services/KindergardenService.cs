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
            var kindergardens = _kindergardenRepository
                .GetAll()
                .Select(new KindergardenMapper().DtoFromEntity);

            return kindergardens;
        }

        public IEnumerable<string> GetAllKindergardenCities()
        {
            return _kindergardenRepository.GetAllCities();
        }

        public IEnumerable<string> GetAllActiveCities()
        {
            return _kindergardenRepository.GetAllActiveCities();
        }

        public IEnumerable<KindergardenDto> GetKindergardensByCity(string city)
        {
            KindergardenMapper kindergardenMapper = new KindergardenMapper();
            return _kindergardenRepository.GetKindergardensByCity(city)
                                          .Select(kindergardenMapper.DtoFromEntity);
        }

        public IEnumerable<KindergardenDto> GetToKindergardens(int requestId)
        {
            KindergardenMapper kindergardenMapper = new KindergardenMapper();
            return _kindergardenRepository.GetToByRequestId(requestId)
                                          .Select(kindergardenMapper.DtoFromEntity);
        }
    }
}
