using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Entith.AspNet.Domain
{
    public abstract class DomainService<TEntity, TKey, TRepository> : DomainService<TEntity, TKey>, IDomainService<TEntity, TKey, TRepository>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TRepository : IRepository<TEntity, TKey>
    {
        protected TRepository CustomRepository { get; private set; }

        public DomainService(IUnitOfWork uow)
            : base(uow)
        {
            CustomRepository = uow.GetRepository<TEntity, TRepository>();
        }
    }

    /// <summary>
    /// Basic implementation of the IDomainService interface. Can be used
    /// as is or extended if more advanced methods are needed or you need
    /// to add domain logic.
    /// </summary>
    public class DomainService<TEntity, TKey> : IDomainService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region ctor + fields

        /// <summary>
        /// The repository instance this service wraps around.
        /// </summary>
        protected IRepository<TEntity, TKey> Repository { get; private set; }

        /// <summary>
        /// The unit of work this service is bound to.
        /// </summary>
        protected IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entith.AspNet.Domain.DomainService`2"/> class.
        /// </summary>
        /// <param name="uow">The unit of work instance to bind to.</param>
        public DomainService(IUnitOfWork uow)
        {
            Repository = uow.GetRepository<TEntity, IRepository<TEntity, TKey>>();
            uow.RegisterService(this);
            _uow = uow;
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
            Repository.AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            Repository.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Repository.RemoveRange(entities);
        }

        #endregion

        #region UOW + domain logic

        public virtual SaveChangesResults SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public virtual ICollection<SaveChangesResult> OnSaveChanges()
        {
            return null;
        }

        public virtual ICollection<SaveChangesResult> PostSaveChanges()
        {
            return null;
        }

        #endregion
    }
}

