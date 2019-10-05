using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private IMatchRepository _matchRepository;
        private IPendingRequestRepository _pendingRequestRepository;
        
        public MatchService(IMatchRepository repository, IPendingRequestRepository pending)
        {
            _matchRepository = repository;
            _pendingRequestRepository = pending;
        }

        public int GetTotalCount()
        {
            return _matchRepository.GetAll().Count();
        }

        public void TryMatch(int id)
        {
            //IncomingRequest = _pendingRequest.Get(id);
            //AllPossibleMatchedRequests = _pendingRequest
            //    .GetAll()
            //    .OrderBy(pr => pr.SubmittedAt)
            //    .Where(pr =>
            //        pr.ToKindergardenWishes.Contains(IncomingRequest.FromKinderdenId) &&
            //        IncomingRequest.ToKindergardenWishes.Contains(pr.FromKindergardenId)
            //    );

            //var finalMatch;

            //for(WishId in IncomingRequest.ToKindergardenWishIds.OrderBy(id => id ASC))
            //{
            //    finalMatch = AllPossibleMatchedRequests.Where(pr => pr.FromKindergarndenId == WishId).FirstOrDefault();
            //    if (firstMatch)
            //        break;
            //}

            PendingRequest match = FindBestMatch(id);

            if (match == null)
                return;

            // TODO: Notify, convert, match
        }

        private PendingRequest FindBestMatch(int id)
        {
            PendingRequest incomingRequest = _pendingRequestRepository.Get(id);

            IEnumerable<PendingRequest> possibleMatches = _pendingRequestRepository
                .GetAllMatchesFor(incomingRequest)
                .OrderBy(possibleMatch => possibleMatch.SubmittedAt);

            foreach (var wishId in incomingRequest.KindergardenWishIds.OrderByDescending(wishId => wishId))
            {
                PendingRequest match = possibleMatches
                    .Where(possibleMatch => possibleMatch.FromKindergardenId == wishId)
                    .FirstOrDefault();

                if (match != null)
                    return match;
            }

            return null;
        }

        public void Unmatch(int id)
        {
            throw new NotImplementedException();
        }
    }
}
