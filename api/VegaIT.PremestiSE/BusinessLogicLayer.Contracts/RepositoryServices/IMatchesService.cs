using System;
using DataAccessLayer.Contracts.Domain;

namespace BusinessLogicLayer.Contracts.RepositoryServices
{
    public interface IMatchesService : IRepositoryService<Match, Tuple<int, int>>
    {
    }
}