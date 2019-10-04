using DataAccessLayer.Contracts.Domain;
using System;

namespace BusinessLogicLayer.Contracts.RepositoryServices
{
    public interface IRequestKindergardenService : IRepositoryService<RequestKindergarden, Tuple<int, int>>
    {
    }
}
