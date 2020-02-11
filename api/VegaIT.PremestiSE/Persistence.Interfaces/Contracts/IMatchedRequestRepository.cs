using System.Collections.Generic;
using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchedRequestRepository
    {
        MatchedRequest Get(int id);
        IEnumerable<MatchedRequest> GetAll();
        MatchedRequest Create(Request request, int matchId);
        MatchedRequest Delete(int id);
    }
}
