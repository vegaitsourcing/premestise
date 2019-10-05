using Persistence.Interfaces.Entites;
using System.Collections.Generic;

namespace Persistence.Interfaces.Contracts
{
    public interface IKindergardenRepository
    {
        Kindergarden GetById(int id);
        IEnumerable<Kindergarden> GetAll();
    }
}
