using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using System;
using System.Linq;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private IMatchRepository _matchRepository;
        private IPendingRequestRepository _pendingRequest;
        
        public MatchService(IMatchRepository repository, IPendingRequestRepository pending)
        {
            _matchRepository = repository;
            _pendingRequest = pending;
        }

        public int GetTotalCount()
        {
            return _matchRepository.GetAll().Count();
        }

        public void TryMatch(int id)
        {
            var matchRequest = _matchRepository.GetAll()
                 .Where(m => m.Id == id).ToList()[0].FirstMatchedRequest;

            var otherRequest = _matchRepository.GetAll()
                .Where(m => m.Id != id)
                .Where(m => matchRequest.KindergardenWishIds.Contains(m.FirstMatchedRequest.FromKindergardenId) &&
                 m.FirstMatchedRequest.KindergardenWishIds.Contains(matchRequest.FromKindergardenId));
            




            
        }

        public void Unmatch(int id)
        {
            throw new NotImplementedException();
        }
    }
}
