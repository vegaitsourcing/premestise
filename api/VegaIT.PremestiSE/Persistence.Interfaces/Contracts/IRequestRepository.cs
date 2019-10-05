using Persistence.Interfaces.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Interfaces.Contracts
{
    public interface IRequestRepository<T> where T : Request
    {
        T Create(T request);
        void Delete(T request);
        T GetById(int id);
    }
}
