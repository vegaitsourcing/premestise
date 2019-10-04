using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
         T GetById(int id);
    }
}