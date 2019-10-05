using System.Collections.Generic;
using Persistence.Interfaces.Entites;

namespace Core.Interfaces.Intefaces
{
    public interface IRequestWishService
    {
        void CreateWishes(List<PendingRequestWishes> pendingRequestWishes);
        void Delete(PendingRequestWishes pendingRequestWish);
    }
}