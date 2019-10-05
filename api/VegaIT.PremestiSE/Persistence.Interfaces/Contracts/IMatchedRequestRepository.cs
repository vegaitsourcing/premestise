using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchedRequestRepository
    {
        MatchedRequest Create(MatchedRequest request);
        MatchedRequest Delete(int id);
    }
}
