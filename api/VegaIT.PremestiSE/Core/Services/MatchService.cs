using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IKindergardenRepository _kindergardenRepository;
        private readonly IPendingRequestRepository _pendingRequestRepository;

        public MatchService(IKindergardenRepository kindergardenRepository, IPendingRequestRepository pendingRequestRepository)
        {
            _kindergardenRepository = kindergardenRepository;
            _pendingRequestRepository = pendingRequestRepository;
        }

        public Match TryMatch(int id)
        {
            PendingRequest pendingRequest = _pendingRequestRepository.GetById(id);
            List<Kindergarden> toKindergardenList = _kindergardenRepository.GetToByRequestId(id);
            List<PendingRequest> allPendingRequests = _pendingRequestRepository.GetAllMatchesForRequest(pendingRequest);
            foreach (var current in allPendingRequests)
            {
                if (toKindergardenList.Find(k => k.Id == current.Id) != null 
                    && _kindergardenRepository.GetToByRequestId(current.Id).Find(k => k.Id == pendingRequest.Id) != null)
                    return new Match
                    {
                        FirstMatchedRequest = pendingRequest,
                        SecondMatchedRequest = current
                    };
            }
            return null;
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public void Unmatch(int id)
        {
            throw new NotImplementedException();
        }
    }
}
