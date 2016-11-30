using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace Entith.AspNet.Domain.EntityFramework
{
    public interface IEfRepository<TEntity, TKey> : IEfRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties);
    }

    public interface IEfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {

        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);
    }
}

