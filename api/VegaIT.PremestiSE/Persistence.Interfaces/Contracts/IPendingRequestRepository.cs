﻿using System.Collections.Generic;
using Persistence.Interfaces.Entites;

namespace Persistence.Interfaces.Contracts
{
    public interface IPendingRequestRepository
    {
        IEnumerable<PendingRequest> GetAll();

        PendingRequest Get(int id);
        PendingRequest Create(Request request);
        IEnumerable<PendingRequest> GetAllMatchesFor(PendingRequest request);
        PendingRequest GetLatest();
        void Delete(int id);
        void Verify(int id);
    }
}