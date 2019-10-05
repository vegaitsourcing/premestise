using Persistence.Interfaces.Entites;
using System.Collections.Generic;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAll();
        Match Create(int firstMatchedRequestId, int secondMatchedRequestId);
        void DeleteByRequestId(int id);
    }
}
