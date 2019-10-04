using System.Collections.Generic;

namespace BusinessLogicLayer.Contracts.RepositoryServices
{
    public interface IRepositoryService<TEntity, TPKey> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TPKey id);
        TEntity Create(TEntity model);
        TEntity Update(TEntity model);
        void Delete(TPKey id);

        //bool Validate();
    }
}
