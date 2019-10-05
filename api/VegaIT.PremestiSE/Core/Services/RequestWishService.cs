using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace Core.Services
{
    public class RequestWishService : IRequestWishService
    {
        private readonly IPendingRequestWishRepository _requestWishRepository;

        public RequestWishService(IPendingRequestWishRepository requestWishRepository)
        {
            _requestWishRepository = requestWishRepository;
        }

        public void CreateWishes(List<PendingRequestWishes> pendingRequestWishes)
        {
            _requestWishRepository.CreateWishes(pendingRequestWishes);
        }

        public void Delete(PendingRequestWishes pendingRequestWish)
        {
            throw new NotImplementedException();
        }
    }
}
