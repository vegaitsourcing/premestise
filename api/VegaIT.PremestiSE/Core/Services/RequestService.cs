using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.Clients;
using Core.Services.Mappers;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Util;

namespace Core.Services
{
    public class RequestService : IRequestService
    {

        private readonly IPendingRequestRepository _pendingRequestRepository;
        private readonly IMatchedRequestRepository _matchedRequestRepository;
        private readonly IMailClient _mailClient;
        private readonly IKindergardenRepository _kindergardenRepository;

        public RequestService(IPendingRequestRepository pendingRequestRepository, IKindergardenRepository kindergardenRepository, IMatchedRequestRepository matchedRequestRepository, IMailClient mailClient)

        {
            _pendingRequestRepository = pendingRequestRepository;
            _kindergardenRepository = kindergardenRepository;
            _matchedRequestRepository = matchedRequestRepository;
            _mailClient = mailClient;
        }

        public RequestDto CreatePending(RequestDto newRequest)
        {   //Kad se kreira pending request treba da se kreira i entry u pending_request_wishes
            var requestMapper = new RequestMapper();
            var kindergardenMapper = new KindergardenMapper();

            if (newRequest.ToKindergardenIds == null)
                newRequest.ToKindergardenIds = new List<string>(0);
            var pendingRequestToAdd = requestMapper.DtoToEntity(newRequest);

            PendingRequest addedPendingRequest = _pendingRequestRepository.Create(pendingRequestToAdd);
            RequestDto addedPendingRequestDto = requestMapper.DtoFromEntity(addedPendingRequest);

            Kindergarden fromKindergarden = _kindergardenRepository.GetById(addedPendingRequest.FromKindergardenId);

            List<KindergardenDto> wishes = new List<KindergardenDto>();
            foreach(var wishId in addedPendingRequest.KindergardenWishIds)
            {
                wishes.Add(kindergardenMapper.DtoFromEntity(_kindergardenRepository.GetById(wishId)));
            }

            _mailClient.SendVerificationMessage(addedPendingRequestDto,
                                                kindergardenMapper.DtoFromEntity(fromKindergarden),
                                                wishes);

            return addedPendingRequestDto;
        }


        public WishDto GetLatest()
        {
            PendingRequest latestPendingRequest = _pendingRequestRepository.GetLatest();
            
            // latestPendingRequest moze biti null ako ne postoji verified pending request, obratiti paznju na to ispod;

            Kindergarden fromKindergarden = _kindergardenRepository.GetById(latestPendingRequest.FromKindergardenId);
            List<Kindergarden> toKindergardens = _kindergardenRepository.GetToByRequestId(latestPendingRequest.Id);


            KindergardenDto fromKindergardenDto = new KindergardenMapper().DtoFromEntity(fromKindergarden);
            IEnumerable<KindergardenDto> toKindergadenDtos =
                toKindergardens.Select(new KindergardenMapper().DtoFromEntity);

            return new WishDto
            { 
                RequestId = latestPendingRequest.Id,
                ChildBirthDate = latestPendingRequest.ChildBirthDate,
                FromKindergarden = fromKindergardenDto,
                ToKindergardens = toKindergadenDtos
            };

        }

        public void DeletePending(int id)
        {
            _pendingRequestRepository.Delete(id);
        }


        public void DeleteMatched(int id)
        {
            _matchedRequestRepository.Delete(id);
        }

        public IEnumerable<RequestDto> GetAllMatched()
        {
            RequestMapper requestMapper = new RequestMapper();

            return _matchedRequestRepository
                .GetAll()
                .Select(requestMapper.DtoFromEntity);
        }

        public IEnumerable<RequestDto> GetAllPending()
        {
            RequestMapper requestMapper = new RequestMapper();

            return _pendingRequestRepository
                    .GetAll()
                    .Where(request => request.Verified)
                    .Select(requestMapper.DtoFromEntity);

        }

        public IEnumerable<WishDto> GetAllPendingWishes()
        {
            return _pendingRequestRepository
                    .GetAll()
                    .Where(request => request.Verified)
                    .Select(request => {
                        Kindergarden fromKindergarden = _kindergardenRepository.GetById(request.FromKindergardenId);
                        List<Kindergarden> toKindergardens = _kindergardenRepository.GetToByRequestId(request.Id);

                        KindergardenDto fromKindergardenDto = new KindergardenMapper().DtoFromEntity(fromKindergarden);
                        IEnumerable<KindergardenDto> toKindergadenDtos =
                            toKindergardens.Select(new KindergardenMapper().DtoFromEntity);

                        return new WishDto
                        {
                            RequestId = request.Id,
                            ChildBirthDate = request.ChildBirthDate,
                            FromKindergarden = fromKindergardenDto,
                            ToKindergardens = toKindergadenDtos
                        };
                    });

        }
    }
}
