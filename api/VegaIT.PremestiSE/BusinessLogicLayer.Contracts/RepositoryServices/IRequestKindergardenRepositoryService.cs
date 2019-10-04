using DataAccessLayer.Contracts.Domain;
using System;

namespace BusinessLogicLayer.Contracts.RepositoryServices
{
    public interface IRequestKindergardenRepositoryService : IRepositoryService<RequestKindergarden, Tuple<int, int>>
    {
    }
}
