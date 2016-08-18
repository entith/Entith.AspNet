using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public interface ILogicUnit<TEntity, TKey> : ILogicUnit
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        void OnAdd(TEntity entity);
        void OnRemove(TEntity entity);
        void PostAdd(TEntity entity);
        void PostRemove(TEntity entity);
    }

    public interface ILogicUnit
    {
        void OnSaveChanges();
        void PostSaveChanges();
    }
}
