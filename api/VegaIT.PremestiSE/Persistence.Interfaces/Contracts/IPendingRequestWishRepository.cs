using System;
using System.Collections.Generic;
using System.Text;
using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IPendingRequestWishRepository
    {
        void CreateWishes(List<PendingRequestWishes> pendingRequestWishes);
        void Delete(PendingRequestWishes pendingRequestWish);
    }
}
