using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces.Intefaces;
using Core.Services;
using Persistence.Interfaces.Entites;
using UnitTest.Fluent.Mocks;
using Xunit;

namespace UnitTest.Services
{
    public class KinderGardenServiceTest
    {
        private IKindergardenService _kindergardenService;

        [Fact]
        public void GivenAvailableKindergardens_WhenGetAllCalled_ShouldReturnKindergardenList()
        {
            var kindergarden = new Kindergarden();
            var kindergardens = new List<Kindergarden>{kindergarden};
            _kindergardenService = new KindergardenService(
                new MockKindergardenRepository()
                    .GetAll(kindergardens)
                    .Object);

            var kindergardenDtos = _kindergardenService.GetAll();

            Assert.Single(kindergardenDtos);
        }

        [Fact]
        public void GivenNoKindergardens_WhenGetAllCalled_ShouldReturnEmptyList()
        {
            _kindergardenService = new KindergardenService(new MockKindergardenRepository().GetAllEmpty().Object);

            var kindergardenDtos = _kindergardenService.GetAll();
            
            Assert.Empty(kindergardenDtos);
        }
    }
}