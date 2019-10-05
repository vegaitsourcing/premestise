using System.Collections.Generic;
using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IPendingRequestRepository : IRequestRepository<PendingRequest>
    {
        List<PendingRequest> GetAllMatchesForRequest(PendingRequest request);
    }
}
