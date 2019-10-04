using System.Collections.Generic;

namespace DataAccessLayer.Contracts.Contracts
{
    public interface IRepository<T>
    {
        void Create(T entity);

        IEnumerable<T> GetAll();



    }
}