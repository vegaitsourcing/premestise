using BusinessLogicLayer.Contracts.RepositoryServices;
using System.Collections.Generic;

namespace BusinessLogicLayer.RepositoryServices
{
    public abstract class RepositoryServiceBase<TEntity, TPKey> : IRepositoryService<TEntity, TPKey> where TEntity : class
    {
        public abstract IEnumerable<TEntity> GetAll();
        public abstract TEntity Get(TPKey id);
        public abstract TEntity Create(TEntity model);
        public abstract TEntity Update(TEntity model);
        public abstract void Delete(TPKey id);

        public RepositoryServiceBase()
        {
        }

        /// <summary>
        /// Function for model validation rules
        /// </summary>
        /// <param name="model">Model to be validated</param>
        /// <returns></returns>
        protected virtual bool Validate(TEntity model)
        {
            return true;
        }
    }
}
