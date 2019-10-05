using System.Collections.Generic;
using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IPendingRequestRepository
    {
        IEnumerable<PendingRequest> GetAll();

        PendingRequest Get(int id);
        PendingRequest Create(PendingRequest request);
        IEnumerable<PendingRequest> GetAllMatchesFor(PendingRequest request);
        void Delete(int id);
        void Verify(int id);
    }
}