namespace Core.Interfaces.Intefaces
{
    public interface IMatchService
    {
        int GetTotalCount();
        void TryMatch(int id);
        void ConfirmMatch(int id);
        void Unmatch(int id);
    }
}
