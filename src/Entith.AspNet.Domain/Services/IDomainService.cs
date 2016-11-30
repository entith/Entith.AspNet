using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Entith.AspNet.Domain
{
    //public interface IDomainService<TEntity, TKey, TRepository> : IDomainService<TEntity, TKey>
    //    where TEntity : class, IEntity<TKey>
    //    where TKey : IEquatable<TKey>
    //    where TRepository : IRepository<TEntity, TKey>
    //{ }

    public interface IDomainService<TEntity, TKey> : IDomainService<TEntity>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey id);
    }

    public interface IDomainService<TEntity> : IDomainService
        where TEntity : class, IEntity
    {
        #region fetch

        ICollection<TEntity> GetAll();

        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region paged fetch

        IPaged<TEntity> GetAllPaged();

        IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region add/remove

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        #endregion
    }

    public interface IDomainService
    {
        void SaveChanges();
    }
}

