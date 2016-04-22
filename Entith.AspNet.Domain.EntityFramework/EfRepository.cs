using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using Entith.AspNet.Domain;

namespace Entith.AspNet.Domain.EntityFramework
{
    public class EfRepository<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        protected readonly TDbContext Context;

        public EfRepository(TDbContext context)
        {
            Context = context;
        }

        public TEntity Get(TKey id)
        {
            return Context.Set<TEntity>().Where(e => e.Id.Equals(id)).FirstOrDefault();
        }

        public TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties)
        {
            return Context.Set<TEntity>().IncludeProperties(properties).Where(e => e.Id.Equals(id)).FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        {
            return Context.Set<TEntity>().IncludeProperties(properties);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            return Context.Set<TEntity>().Where(predicate).IncludeProperties(properties);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}

