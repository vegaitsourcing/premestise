using System.Collections.Generic;
using DataAccessLayer.Contracts.Domain;
using DataAccessLayer.Contracts.Contracts;
using BusinessLogicLayer.Contracts.RepositoryServices;

namespace BusinessLogicLayer.RepositoryServices
{
    public class KindergardenService : RepositoryServiceBase<Kindergarden, int>, IKindergardenService
    {
        private readonly IKindergardenRepository _repository;

        public KindergardenService(IKindergardenRepository repository)
        {
            _repository = repository;
        }

        public override Kindergarden Create(Kindergarden model)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Kindergarden Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Kindergarden> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public override Kindergarden Update(Kindergarden model)
        {
            throw new System.NotImplementedException();
        }
    }
}
