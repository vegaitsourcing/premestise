using System;
using System.Collections.Generic;
using DataAccessLayer.Contracts.Domain;
using DataAccessLayer.Contracts.Contracts;
using BusinessLogicLayer.Contracts.RepositoryServices;

namespace BusinessLogicLayer.RepositoryServices
{
    public class RequestKindergardenService : RepositoryServiceBase<RequestKindergarden, Tuple<int, int>>, IRequestKindergardenService
    {
        private readonly IRequestKindergardenRepository _repository;

        public RequestKindergardenService(IRequestKindergardenRepository repository)
        {
            _repository = repository;
        }

        public override RequestKindergarden Create(RequestKindergarden model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Tuple<int, int> id)
        {
            throw new NotImplementedException();
        }

        public override RequestKindergarden Get(Tuple<int, int> id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<RequestKindergarden> GetAll()
        {
            throw new NotImplementedException();
        }

        public override RequestKindergarden Update(RequestKindergarden model)
        {
            throw new NotImplementedException();
        }
    }
}
