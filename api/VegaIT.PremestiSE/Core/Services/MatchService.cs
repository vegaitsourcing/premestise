using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private IMatchRepository _matchRepository;
        private IPendingRequestRepository _pendingRequestRepository;
        private readonly IMatchedRequestRepository _matchedRequestRepository;

        public MatchService(IMatchRepository repository, IPendingRequestRepository pending, IMatchedRequestRepository matchedRequestRepository)
        {
            _matchRepository = repository;
            _pendingRequestRepository = pending;
            _matchedRequestRepository = matchedRequestRepository;
        }

        public int GetTotalCount()
        {
            return _matchRepository.GetAll().Count();
        }

        public void TryMatch(int id)
        {

            PendingRequest incomingRequest = _pendingRequestRepository.Get(id);
            PendingRequest match = FindBestMatch(incomingRequest);

            if (match == null)
                return;

            _pendingRequestRepository.Delete(incomingRequest.Id);
            MatchedRequest firstMatchedRequest = _matchedRequestRepository.Create(incomingRequest);

            _pendingRequestRepository.Delete(match.Id);
            MatchedRequest secondMatchedRequest = _matchedRequestRepository.Create(match);

            _matchRepository.Create(firstMatchedRequest, secondMatchedRequest);



        }

        private PendingRequest FindBestMatch(PendingRequest request)
        {

            IEnumerable<PendingRequest> possibleMatches = _pendingRequestRepository
                .GetAllMatchesFor(request)
                .OrderBy(possibleMatch => possibleMatch.SubmittedAt);

            foreach (var wishId in request.KindergardenWishIds.OrderByDescending(wishId => wishId))
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
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    // delete match and return obj
                    MatchedRequest request = _matchedRequestRepository.Delete(id);

                    // convert and save to pending matches -- mozda fali Id ?
                    PendingRequest tempRequest = new PendingRequest()
                    {
                        ChildBirthDate = request.ChildBirthDate,
                        FromKindergardenId = request.FromKindergardenId,
                        KindergardenWishIds = request.KindergardenWishIds,
                        ChildName = request.ChildName,
                        ParentEmail = request.ParentEmail,
                        ParentName = request.ParentName,
                        ParentPhoneNumber = request.ParentPhoneNumber,
                        SubmittedAt = request.SubmittedAt,
                        Verified = true
                    };

                    _pendingRequestRepository.Create(tempRequest);

                    // set match status to Failure
                    _matchRepository.SetStatus(id, Status.Failure);

                    // complete transaction
                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
        }
    }
}
