using System;
using System.Collections.Generic;
using System.Linq;

namespace Entith.AspNet.Domain
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected IEnumerable<IDomainService> _services;
        protected IEnumerable<IRepository> _repositories;

        public UnitOfWork(IEnumerable<IRepository> repositories, IEnumerable<IDomainService> services)
        {
            _services = services;
            _repositories = repositories;

            foreach(var service in services)
            {
                service.Init(this);
            }
        }

        public abstract void Dispose();
        public abstract IEnumerable<IEntity> GetAdded();
        public abstract IEnumerable<IEntity> GetRemoved();
        public abstract IEnumerable<IEntity> GetModified();

        public abstract IEnumerable<TEntity> GetAdded<TEntity>() where TEntity : class, IEntity;
        public abstract IEnumerable<TEntity> GetModified<TEntity>() where TEntity : class, IEntity;
        public abstract IEnumerable<TEntity> GetRemoved<TEntity>() where TEntity : class, IEntity;

        public abstract void ClearChanges(IEntity entity);
        public abstract void ClearAllChanges();

        public virtual void SaveChanges()
        {
            List<SaveChangesResult> result = new List<SaveChangesResult>();

            foreach (IDomainService service in _services)
            {
                service.OnSaveChanges();
            }
        }

        public void PostSaveChanges()
        {
            List<SaveChangesResult> result = new List<SaveChangesResult>();

            foreach (IDomainService service in _services)
            {
                service.PostSaveChanges();
            }
        }

        public TRepository GetRepository<TEntity, TRepository>()
            where TEntity : class, IEntity 
            where TRepository : IRepository<TEntity>
        {
            return _repositories.OfType<TRepository>().FirstOrDefault();
        }

        public IDomainService<TEntity, TKey> GetService<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return _services.OfType<IDomainService<TEntity, TKey>>().FirstOrDefault();
        }
    }
}

