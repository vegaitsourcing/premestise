using Core.Clients;
using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Mappers;
using Core.Interfaces.Models;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPendingRequestRepository _pendingRequestRepository;
        private readonly IMatchedRequestRepository _matchedRequestRepository;
        private readonly IMailClient _mailClient;
        private readonly IKindergardenRepository _kindergardenRepository;
        private readonly int _chainLength;

        public MatchService(IMatchRepository matchRepository, IPendingRequestRepository pendingRequestRepository, IMatchedRequestRepository matchedRequestRepository, IMailClient mailClient, IKindergardenRepository kindergardenRepository, IConfiguration config)
        {
            _matchRepository = matchRepository;
            _pendingRequestRepository = pendingRequestRepository;
            _matchedRequestRepository = matchedRequestRepository;
            _mailClient = mailClient;
            _kindergardenRepository = kindergardenRepository;
            _chainLength = int.Parse(config.GetSection("chainLength").Value);
        }

        public int GetTotalCount()
        {
            // Ne znam da li, kada na sajtu prikazuje ukupan broj uspjesnih premjestaja, treba
            // da prikaze samo one koji su mailom potvrdili da su uspjeli da dogovore premjestaj - SUCCESS
            // ili i one koji su se matchovali ali nisu mailom zvanicno potvrdili (zaboravili su ili sta vec) - MATCHED
            // Zato sam stavio OR
            return _matchRepository.GetAll().Count(m => m.Status == Status.Success || m.Status == Status.Matched);
        }
        public void TryMatch(int id)
        {

            //get all pending requests with verified status
            IEnumerable<PendingRequest> allPending = _pendingRequestRepository.GetAllVerified().OrderBy(pending => pending.SubmittedAt);
            //incoming request
            PendingRequest incomingRequest = _pendingRequestRepository.Get(id);
            _pendingRequestRepository.Verify(id);



            //Potential ways of starting a chain
            IEnumerable<PendingRequest> potentials =
                allPending.Where(pending => pending.KindergardenWishIds.First() == incomingRequest.FromKindergardenId &&
                                            pending.Group == incomingRequest.Group);
            

            int len = potentials.Count();
            //ukoliko nema zahteva koji se moze nakaciti na dolazeci zahtev znaci da nisu ispunjeni uslovi za otpocinjanje kreiranja lanca
            if (potentials.Count() == 0)
                return;
            

            //initial chains
             List<PendingRequest>  chain = new List<PendingRequest>(1);

                chain.Add(incomingRequest); //incoming ide na prvo mesto
                chain.Add(potentials.ElementAt(0)); //prvi vezivni po starini na drugo mesto
                
            

            //if initial two chain elements can meet ends then it is over send them emails

                //trenutno imamo samo dva elementa u lancu proveravamo da li je pun krug ranga 2
                if (chain.First().KindergardenWishIds.First() == chain.Last().FromKindergardenId)
                {
                    sendRotationalMatchEmails(chain);
                }
                else
                {
                    PopulateChain(allPending, chain, _chainLength);
                } 
                                        

            //if chain elements len greater than 2 and chain ends can meet send emails to chain participants

                if(chain.Count() > 2 &&
                    chain.First().KindergardenWishIds.First() ==
                    chain.Last().FromKindergardenId)
                sendRotationalMatchEmails(chain);
                    //mail table
            

        }

        private void PopulateChain(IEnumerable<PendingRequest> allPending, List<PendingRequest> toPopulateChain, int maxChainLength)
        {
            //returns if chain's elements count is less-equal than max chain size and if ends can meet function exits
            if (toPopulateChain.Count() <= maxChainLength &&
                toPopulateChain.Last().FromKindergardenId ==
                toPopulateChain.First().KindergardenWishIds.First())
                return;

            //returns if there is no elements to add to the chain therefore ends can not meet
            PendingRequest newChainElement = allPending.Where(
                pending => pending.KindergardenWishIds.First() == toPopulateChain.Last().FromKindergardenId &&
                           pending.Group == toPopulateChain.Last().Group)
                          .FirstOrDefault();

            //if no more elements for this chain found function returns
            if (newChainElement == null)
                return;

            //if there are elements we can add to the chain we add it to the chain and call same function again
            toPopulateChain.Add(newChainElement);
            PopulateChain(allPending, toPopulateChain, maxChainLength);

        }


        private void sendRotationalMatchEmails(IEnumerable<PendingRequest> validChain)
        {
            Match addedMatch = null;
            MatchedRequest firstMatchedRequest = null;
            MatchedRequest secondMatchedRequest = null;
            PendingRequest currentPending = null;
            PendingRequest incomingRequest = validChain.Last();
            var requestMapper = new RequestMapper();
            var kindergardenMapper = new KindergardenMapper();

            KindergardenDto fromKindergarden = null;
            KindergardenDto toKindergarden = null;

            List<MatchedRequest> chainRequests = null;
            RequestDto firstMatchDto = null;
            RequestDto secondMatchDto = null;

            List<int> directPendingMatchIdsToRemove = new List<int>(2);
            List<int> circularPendingMatchIdsToRemove = new List<int>(12);

            var chainLength = validChain.Count();




            if (chainLength == 2)
            {

                directPendingMatchIdsToRemove.AddRange(validChain.Select(el => el.Id));

                addedMatch = _matchRepository.Create();
                firstMatchedRequest = _matchedRequestRepository.Create(validChain.ElementAt(1), addedMatch.Id);
                secondMatchedRequest = _matchedRequestRepository.Create(validChain.ElementAt(0), addedMatch.Id);

                foreach (int toRemovePendingId in directPendingMatchIdsToRemove)
                {
                    _pendingRequestRepository.Delete(toRemovePendingId);
                }

                fromKindergarden =
                   kindergardenMapper.DtoFromEntity(
                       _kindergardenRepository.GetById(firstMatchedRequest.FromKindergardenId));
                toKindergarden = kindergardenMapper.DtoFromEntity(
                   _kindergardenRepository.GetById(secondMatchedRequest.FromKindergardenId));

                firstMatchDto = requestMapper.DtoFromEntity(firstMatchedRequest);
                secondMatchDto = requestMapper.DtoFromEntity(secondMatchedRequest);


                _mailClient.SendFoundMatchMessage(firstMatchDto,
                                                secondMatchDto,
                                                fromKindergarden,
                                                toKindergarden);

                
            }

            if (chainLength > 2)
            {
                circularPendingMatchIdsToRemove.AddRange(validChain.Select(el => el.Id));

                chainRequests = new List<MatchedRequest>(validChain.Count());
                addedMatch = _matchRepository.Create();

                for (var i = 0; i < validChain.Count(); i++)
                {
                    chainRequests.Add(_matchedRequestRepository.Create(validChain.ElementAt(i), addedMatch.Id));
                }

                foreach (int toRemovePendingId in circularPendingMatchIdsToRemove)
                {
                    _pendingRequestRepository.Delete(toRemovePendingId);
                }

                chainRequests.Reverse();
                _mailClient.SendCircularMatchMessage(chainRequests);



            }

            

            /// TREBA OBRISATI PENDING ZAHTEVE POSLE MATCHA


        }

        //try to populate chain and make ends meet


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
