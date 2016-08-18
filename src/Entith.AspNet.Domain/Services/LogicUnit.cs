using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain.Services
{
    public abstract class LogicUnit<TEntity, TKey> : ILogicUnit<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected IUnitOfWork Uow { get; private set; }
        protected IDomainService<TEntity, TKey> Service { get; private set; }
        protected IRepository<TEntity, TKey> Repository { get; private set; }

        public LogicUnit(IUnitOfWork uow, IDomainService<TEntity, TKey> service, IRepository<TEntity, TKey> repository)
        {
            Uow = uow;
            Service = service;
            Repository = repository;
        }

        public virtual void OnAdd(TEntity entity) { }

        public virtual void OnRemove(TEntity entity) { }

        public virtual void OnSaveChanges() { }

        public virtual void PostAdd(TEntity entity) { }

        public virtual void PostRemove(TEntity entity) { }

        public virtual void PostSaveChanges() { }
    }
}
