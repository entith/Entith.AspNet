using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public interface ILogicUnit<TEntity> : ILogicUnit
        where TEntity : class, IEntity
    {
        void OnAdd(TEntity entity);
        void OnRemove(TEntity entity);
        void PostAdd(TEntity entity);
        void PostRemove(TEntity entity);
    }

    public interface ILogicUnit
    {
        void Init(IRepositoryManager repoManager, IChangeTracker changeTracker);
        void OnSaveChanges();
        bool PostSaveChanges();
        Type GetEntityType();

        void DoAdd(IEntity entity);
        void DoRemove(IEntity entity);
        void DoPostAdd(IEntity entity);
        void DoPostRemove(IEntity entity);
    }
}
