using Persistence.Interfaces.Entites;

namespace Core.Interfaces.Intefaces
{
    public interface IMatchService
    {
        int GetTotalCount();
        Match TryMatch(int id);
        void Unmatch(int id);
    }
}
