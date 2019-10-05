using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchedRequestRepository
    {
        MatchedRequest Get(int id);
        MatchedRequest Create(Request request);
        MatchedRequest Delete(int id);
    }
}
