using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
            var kindergardens = _kindergardenRepository.GetAll();
            foreach (var kindergarden in kindergardens)
            {
                yield return KindergardenDto.FromKindergarden(kindergarden);
            }

        }
    }
}
