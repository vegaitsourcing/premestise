using System.Collections.Generic;
using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IPendingRequestRepository : IRequestRepository<PendingRequest>
    {
        IEnumerable<PendingRequest> GetAllMatchesForRequest(PendingRequest request);
    }
}
