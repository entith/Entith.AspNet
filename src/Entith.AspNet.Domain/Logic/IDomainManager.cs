using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public interface IDomainManager : IRepositoryManager
    {
        void OnAdd<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
        void OnRemove<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
        void PostAdd<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
        void PostRemove<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void OnSaveChanges();
        void PostSaveChanges();
    }
}
