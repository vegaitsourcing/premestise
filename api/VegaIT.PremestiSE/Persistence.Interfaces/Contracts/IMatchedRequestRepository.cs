using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IMatchedRequestRepository
    {
        MatchedRequest Create(Request request);
        MatchedRequest Delete(int id);
    }
}
