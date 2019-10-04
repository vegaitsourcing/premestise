using System;
using DataAccessLayer.Contracts.Domain;

namespace BusinessLogicLayer.Contracts.RepositoryServices
{
    public interface IMatchesRepositoryService : IRepositoryService<Match, Tuple<int, int>>
    {
    }
}