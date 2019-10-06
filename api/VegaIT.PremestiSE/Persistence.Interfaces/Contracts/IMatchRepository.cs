using Persistence.Interfaces.Entites;
using System.Collections.Generic;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAll();
        Match Create();
        void DeleteByRequestId(int id);

        void SetStatus(int id, Status status);
    }
}