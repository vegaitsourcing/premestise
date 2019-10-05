using Persistence.Interfaces.Entites;
using System.Collections.Generic;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAll();
        Match Create(MatchedRequest firstMatchedRequest, MatchedRequest secondMatchedRequest);
        void DeleteByRequestId(int id);
    }
}
