using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Castle.DynamicProxy;

namespace Entith.AspNet.Domain
{
    public abstract class DomainService<TEntity, TKey, TRepository> : DomainService<TEntity, TKey>//, IDomainService<TEntity, TKey, TRepository>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TRepository : class, IRepository<TEntity, TKey>
    {
        protected TRepository CustomRepository { get; private set; }

        public DomainService(IUnitOfWork uow, IRepositoryManager repositoryManager)
            :base (uow, repositoryManager)
        {
            CustomRepository = RepositoryManager.GetRepository<TEntity, TRepository>();
        }
    }

    public class DomainService<TEntity, TKey> : IDomainService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region ctor + fields

        protected IRepository<TEntity, TKey> Repository { get; private set; }

        protected IUnitOfWork Uow { get; private set; }

        protected IRepositoryManager RepositoryManager { get; set; }
        
        public DomainService(IUnitOfWork uow, IRepositoryManager repositoryManager)
        {
            Uow = uow;
            RepositoryManager = repositoryManager;

            Repository = RepositoryManager.GetRepository<TEntity, IRepository<TEntity, TKey>>();
        }

        #endregion

        #region fetch

        public virtual ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Find(predicate).ToList();
        }

        //public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Find(predicate, properties).ToList();
        //}

        public virtual TEntity Get(TKey id)
        {
            return Repository.Get(id);
        }

        //public TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Get(id, properties);
        //}

        public virtual ICollection<TEntity> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        //public ICollection<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.GetAll(properties).ToList();
        //}

        #endregion

        #region paged fetch

        public virtual IPaged<TEntity> GetAllPaged()
        {
            return Repository.GetAll().ToPaged();
        }

        //public IPaged<TEntity> GetAllPaged(params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.GetAll(properties).ToPaged();
        //}

        public virtual IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Find(predicate).ToPaged();
        }

        //public IPaged<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        //{
        //    return _repository.Find(predicate, properties).ToPaged();
        //}

        #endregion

        #region add/remove

        public virtual void Add(TEntity entity)
        {
            Repository.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Repository.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                Remove(entity);
        }

        #endregion

        #region UOW

        public void SaveChanges()
        {
            Uow.SaveChanges();
        }

        #endregion
    }
}

