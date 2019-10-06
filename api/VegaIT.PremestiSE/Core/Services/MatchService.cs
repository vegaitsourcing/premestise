using Core.Clients;
using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Mappers;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPendingRequestRepository _pendingRequestRepository;
        private readonly IMatchedRequestRepository _matchedRequestRepository;
        private readonly IMailClient _mailClient;
        private readonly IKindergardenRepository _kindergardenRepository;

        public MatchService(IMatchRepository matchRepository, IPendingRequestRepository pendingRequestRepository, IMatchedRequestRepository matchedRequestRepository, IMailClient mailClient, IKindergardenRepository kindergardenRepository)
        {
            _matchRepository = matchRepository;
            _pendingRequestRepository = pendingRequestRepository;
            _matchedRequestRepository = matchedRequestRepository;
            _mailClient = mailClient;
            _kindergardenRepository = kindergardenRepository;
        }

        public int GetTotalCount()
        {
            return _matchRepository.GetAll().Count();
        }

        public void TryMatch(int id)
        {
            PendingRequest incomingRequest = _pendingRequestRepository.Get(id);
            _pendingRequestRepository.Verify(id);

            PendingRequest match = FindBestMatch(incomingRequest);

            if (match == null) return;

            Match addedMatch = _matchRepository.Create();

            _pendingRequestRepository.Delete(incomingRequest.Id);
            MatchedRequest firstMatchedRequest = _matchedRequestRepository.Create(incomingRequest, addedMatch.Id);

            _pendingRequestRepository.Delete(match.Id);
            MatchedRequest secondMatchedRequest = _matchedRequestRepository.Create(match, addedMatch.Id);

            var requestMapper = new RequestMapper();
            var kindergardenMapper = new KindergardenMapper();
            var fromKindergarden =
                kindergardenMapper.DtoFromEntity(
                    _kindergardenRepository.GetById(firstMatchedRequest.FromKindergardenId));
            var toKindergarden = kindergardenMapper.DtoFromEntity(
                _kindergardenRepository.GetById(secondMatchedRequest.FromKindergardenId));

            var firstMatchDto = requestMapper.DtoFromEntity(firstMatchedRequest);
            var secondMatchDto = requestMapper.DtoFromEntity(secondMatchedRequest);

            _mailClient.SendFoundMatchMessage(firstMatchDto,
                                            secondMatchDto,
                                            fromKindergarden,
                                            toKindergarden);
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

        public int Unmatch(int id)
        {
            // delete match and return obj
            MatchedRequest request = _matchedRequestRepository.Delete(id);

            // convert and save to pending matches
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
            _pendingRequestRepository.Verify(id);

            PendingRequest addedRequest = _pendingRequestRepository.Create(tempRequest);

            // set match status to Failure
            _matchRepository.SetStatus(request.MatchId, Status.Failure);


            return addedRequest.Id;
            // complete transaction
        }

        public void ConfirmMatch(int id)
        {
            MatchedRequest matchedReq = _matchedRequestRepository.Get(id);
            // set match status to Success
            _matchRepository.SetStatus(matchedReq.MatchId, Status.Success);
        }
    }
}
