using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchedRequestRepository
    {
        MatchedRequest Create(PendingRequest request);
        MatchedRequest Delete(int id);
    }
}
