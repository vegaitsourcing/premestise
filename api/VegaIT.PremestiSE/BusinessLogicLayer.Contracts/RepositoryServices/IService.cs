using System.Collections.Generic;

namespace BusinessLogicLayer.Contracts.RepositoryServices
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T model);
        T Update(T model);
        void Delete(int id);

        //bool Validate();
    }
}
