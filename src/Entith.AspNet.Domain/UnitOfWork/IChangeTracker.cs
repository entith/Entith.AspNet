using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    public interface IChangeTracker
    {
        IEnumerable<IEntity> GetAdded();

        IEnumerable<IEntity> GetModified();

        IEnumerable<IEntity> GetRemoved();

        IEnumerable<TEntity> GetAdded<TEntity>() where TEntity : class, IEntity;

        IEnumerable<TEntity> GetModified<TEntity>() where TEntity : class, IEntity;

        IEnumerable<TEntity> GetRemoved<TEntity>() where TEntity : class, IEntity;
    }
}
