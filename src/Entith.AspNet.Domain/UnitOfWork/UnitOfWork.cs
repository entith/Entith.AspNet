using System;
using System.Collections.Generic;
using System.Linq;

namespace Entith.AspNet.Domain
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private IDomainManager _domainManager;

        public void RegisterDomainManager(IDomainManager manager)
        {
            _domainManager = manager;
        }

        public abstract void Dispose();

        public abstract IEnumerable<IEntity> GetTracked();
        public abstract IEnumerable<IEntity> GetAdded();
        public abstract IEnumerable<IEntity> GetRemoved();
        public abstract IEnumerable<IEntity> GetModified();

        public abstract IEnumerable<TEntity> GetTracked<TEntity>() where TEntity : class, IEntity;
        public abstract IEnumerable<TEntity> GetAdded<TEntity>() where TEntity : class, IEntity;
        public abstract IEnumerable<TEntity> GetModified<TEntity>() where TEntity : class, IEntity;
        public abstract IEnumerable<TEntity> GetRemoved<TEntity>() where TEntity : class, IEntity;

        public abstract void ClearChanges(IEntity entity);
        public abstract void ClearAllChanges();

        public virtual void SaveChanges()
        {
            if (_domainManager != null)
                _domainManager.OnSaveChanges();

            PerformSaveChanges();

            if (_domainManager != null)
                _domainManager.PostSaveChanges();
        }

        protected abstract void PerformSaveChanges();
    }
}

