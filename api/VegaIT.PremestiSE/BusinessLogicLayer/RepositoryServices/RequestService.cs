using System;
using System.Collections.Generic;
using DataAccessLayer.Contracts.Domain;
using DataAccessLayer.Contracts.Contracts;
using BusinessLogicLayer.Contracts.RepositoryServices;

namespace BusinessLogicLayer.RepositoryServices
{
    public class RequestService : RepositoryServiceBase<Request, int>, IRequestService
    {
        private readonly IRequestRepository _repository;

        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }

        public override Request Get(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Request> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Request Create(Request model)
        {
            throw new NotImplementedException();
        }

        public override Request Update(Request model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
