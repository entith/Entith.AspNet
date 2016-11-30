using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey id);
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }

    public interface IRepository
    {
    }
}

