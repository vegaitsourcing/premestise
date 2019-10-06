namespace Core.Interfaces.Intefaces
{
    public interface IMatchService
    {
        int GetTotalCount();
        void TryMatch(int id);
        void ConfirmMatch(int id);
        int Unmatch(int id);
    }
}
