using System.Collections.Generic;
using Core.Interfaces.Intefaces;
using Core.Services;
using Persistence.Interfaces.Entites;
using UnitTest.Fluent.Mocks;
using Xunit;

namespace UnitTest.Services
{
    public class MatchServiceTest
    {
        private IMatchService _matchService;
        private readonly MockMatchRepository _matchRepo;
        private readonly MockMatchedRequestRepository _matchedRepo;
        private readonly MockPendingRequestRepository _pendingRequestRepo;

        public MatchServiceTest()
        {
            _matchRepo = new MockMatchRepository();
            _matchedRepo = new MockMatchedRequestRepository();
            _pendingRequestRepo = new MockPendingRequestRepository();
        }

        //[Fact]
        //public void GivenTwoTotalMatches_WhenGetTotalCountCalled_ShouldReturnTwo()
        //{
        //    var twoMatches = new List<Match> {new Match(), new Match()};
        //    _matchService = new MatchService(new MockMatchRepository().GetAll(twoMatches).Object, null, null, null);
            
        //    Assert.Equal(2, _matchService.GetTotalCount());
        //}

        [Fact]
        public void bla()
        {
            
            _matchService = new MatchService(_matchRepo.Object, _pendingRequestRepo.Object, _matchedRepo.Object, null, null, null);
        }
    }
}